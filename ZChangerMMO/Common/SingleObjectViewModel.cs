
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZChangerMMO.Common.Utils;

namespace ZChangerMMO.Common
{
    /// <summary>
    /// The base class for POCO view models exposing a single entity of a given type and CRUD operations against this entity.
    /// This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public abstract partial class SingleObjectViewModel<TEntity, TPrimaryKey, TUnitOfWork> : SingleObjectViewModelBase<TEntity, TPrimaryKey, TUnitOfWork>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork
    {
        readonly Dictionary<string, IDocumentContent> lookUpViewModels = new Dictionary<string, IDocumentContent>();

        /// <summary>
        /// Initializes a new instance of the SingleObjectViewModel class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create the unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        /// <param name="getEntityDisplayNameFunc">An optional parameter that provides a function to obtain the display text for a given entity. If ommited, the primary key value is used as a display text.</param>
        protected SingleObjectViewModel(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc, Func<TEntity, object> getEntityDisplayNameFunc = null)
            : base(unitOfWorkFactory, getRepositoryFunc, getEntityDisplayNameFunc)
        {
        }

        protected CollectionViewModel<TDetailEntity, TDetailPrimaryKey, TUnitOfWork> GetDetailsCollectionViewModel<TViewModel, TDetailEntity, TDetailPrimaryKey, TForeignKey>(
            Expression<Func<TViewModel, CollectionViewModel<TDetailEntity, TDetailPrimaryKey, TUnitOfWork>>> propertyExpression,
            Func<TUnitOfWork, IRepository<TDetailEntity, TDetailPrimaryKey>> getRepositoryFunc,
            Expression<Func<TDetailEntity, TForeignKey>> foreignKeyExpression,
            Action<TDetailEntity, TPrimaryKey> setMasterEntityKeyAction,
            Func<IRepositoryQuery<TDetailEntity>, IQueryable<TDetailEntity>> projection = null) where TDetailEntity : class
        {
            return GetCollectionViewModelCore<CollectionViewModel<TDetailEntity, TDetailPrimaryKey, TUnitOfWork>, TDetailEntity, TDetailEntity, TForeignKey>(propertyExpression,
                () => CollectionViewModel<TDetailEntity, TDetailPrimaryKey, TUnitOfWork>.CreateCollectionViewModel(UnitOfWorkFactory, getRepositoryFunc, AppendForeignKeyPredicate<TDetailEntity, TDetailEntity, TForeignKey>(foreignKeyExpression, projection), CreateForeignKeyPropertyInitializer(setMasterEntityKeyAction, () => PrimaryKey), () => CanCreateNewEntity(), true));
        }

        Func<IRepositoryQuery<TDetailEntity>, IQueryable<TDetailProjection>> AppendForeignKeyPredicate<TDetailEntity, TDetailProjection, TForeignKey>(
            Expression<Func<TDetailEntity, TForeignKey>> foreignKeyExpression,
            Func<IRepositoryQuery<TDetailEntity>, IQueryable<TDetailProjection>> projection)
            where TDetailEntity : class
            where TDetailProjection : class
        {
            var predicate = ExpressionHelper.GetValueEqualsExpression(foreignKeyExpression, (TForeignKey)(object)PrimaryKey);
            return ReadOnlyRepositoryExtensions.AppendToProjection(predicate, projection);
        }

        Action<TDetailEntity> CreateForeignKeyPropertyInitializer<TDetailEntity, TForeignKey>(Action<TDetailEntity, TPrimaryKey> setMasterEntityKeyAction, Func<TForeignKey> getMasterEntityKey) where TDetailEntity : class
        {
            return x => setMasterEntityKeyAction(x, (TPrimaryKey)(object)getMasterEntityKey());
        }

        TViewModel GetEntitiesViewModelCore<TViewModel, TDetailEntity>(LambdaExpression propertyExpression, Func<TViewModel> createViewModelCallback)
            where TViewModel : IDocumentContent
            where TDetailEntity : class
        {

            IDocumentContent result = null;
            string propertyName = ExpressionHelper.GetPropertyName(propertyExpression);
            if (!lookUpViewModels.TryGetValue(propertyName, out result))
            {
                result = createViewModelCallback();
                lookUpViewModels[propertyName] = result;
            }
            return (TViewModel)result;
        }

        TViewModel GetCollectionViewModelCore<TViewModel, TDetailEntity, TDetailProjection, TForeignKey>(
                LambdaExpression propertyExpression,
                Func<TViewModel> createViewModelCallback)
                where TViewModel : IDocumentContent
                where TDetailEntity : class
                where TDetailProjection : class
        {
            return GetEntitiesViewModelCore<TViewModel, TDetailProjection>(propertyExpression, () =>
            {
                var viewModel = createViewModelCallback();
                viewModel.SetParentViewModel(this);
                return viewModel;
            });
        }
    }
}