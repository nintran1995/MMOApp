﻿using Autofac;
using ZChangerMMO.Domain;
using ZChangerMMO.UI;

namespace ZChangerMMO.Common
{
    public class EntityViewModel<T> : BaseViewModel
    {
        protected IUoW _uoW;
        public T Item
        {
            get { return GetProperty(() => Item); }
            private set { SetProperty(() => Item, value); }
        }

        public EntityViewModel()
        {
            _uoW = Bootstrapper.Container.Resolve<IUoW>();
        }

        protected virtual void SetItem(T item)
        {
            Item = item;
        }
    }
}
