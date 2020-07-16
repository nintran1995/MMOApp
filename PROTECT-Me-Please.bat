@CLS
@ECHO ZCHANGER MMO RELEASE MAKER

@RMDIR /Q /S "ZChangerMMO\bin\Release\ZChangerMMO_Secure"
"C:\Program Files (x86)\Eziriz\.NET Reactor\dotNET_Reactor.Console.exe" -file "E:\Projects\Browser\ZChanger\ZChangerMMO\ZChangerMMO\bin\Release\ZChangerMMO.exe" -antitamp 1 -anti_debug 1 -hide_calls 1 -control_flow_obfuscation 1 -flow_level 9 -resourceencryption 1 -antistrong 1 -virtualization 1 -obfuscation 0
@COPY "ZChangerMMO\bin\Release\ZChangerMMO_Secure\ZChangerMMO.exe" "ZChangerMMO\bin\Release\ZChangerMMO.exe" 
@RMDIR /Q /S "ZChangerMMO\bin\Release\ZChangerMMO_Secure"

@RMDIR /Q /S "ZChangerMMO\bin\Release\ZChangerMMOHost_Secure"
"C:\Program Files (x86)\Eziriz\.NET Reactor\dotNET_Reactor.Console.exe" -file "E:\Projects\Browser\ZChanger\ZChangerMMO\ZChangerMMO\bin\Release\ZChangerMMOHost.exe" -antitamp 1 -anti_debug 1 -hide_calls 1 -control_flow_obfuscation 1 -flow_level 9 -resourceencryption 1 -antistrong 1 -virtualization 1 -all_params 1 -naming stealth -exclude_compiler_types 0 -include_compiler_serializable_types 0
@COPY "ZChangerMMO\bin\Release\ZChangerMMOHost_Secure\ZChangerMMOHost.exe" "ZChangerMMO\bin\Release\ZChangerMMOHost.exe" 
@RMDIR /Q /S "ZChangerMMO\bin\Release\ZChangerMMOHost_Secure"


@DEL "ZChangerMMO\bin\Release\*.xml"
@DEL "ZChangerMMO\bin\Release\*.pdb"

@ECHO Creating manual update file
@COPY "ZChangerMMO\bin\Release\ZChangerMMO.exe" "Setup\Main\Update\ZChangerMMO.exe" 
@COPY "ZChangerMMO\bin\Release\ZChangerMMOHost.exe" "Setup\Main\Update\ZChangerMMOHost.exe" 

REM @ECHO Creating setup file
REM "C:\Program Files (x86)\Actual Installer\actinst.exe" /S "E:\Projects\Browser\ZChanger\ZChangerMMO\Setup\Main\ZChangerMMOSetup.aip"

REM @START %windir%\explorer.exe "E:\Projects\Windows\ZChanger-Win-BTT\bin\Release\Setup\"

@ECHO Done!!
