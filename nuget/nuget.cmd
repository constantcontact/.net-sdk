%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ..\CTCTWrapper\CTCTWrapper.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

rd /s /q packages

mkdir packages\lib\net40\

copy ..\CTCTWrapper\bin\Release\CTCT.dll packages\lib\net40
copy ..\CTCTWrapper\bin\Release\CTCT.pdb packages\lib\net40
copy ..\CTCTWrapper\bin\Release\CTCT.dll.config packages\lib\net40

nuget.exe update -self
nuget.exe pack CTCTWrapper.nuspec -Symbols -BasePath packages
