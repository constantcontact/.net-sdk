%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ..\CTCTWrapper\CTCTWrapper.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

rd /s /q packages

mkdir packages\lib\net40\

copy ..\CTCTWrapper\bin\Release\CTCTWrapper.dll packages\lib\net40
copy ..\CTCTWrapper\bin\Release\CTCTWrapper.pdb packages\lib\net40
copy ..\CTCTWrapper\bin\Release\CTCTWrapper.dll.config packages\lib\net40

nuget.exe update -self
nuget.exe pack CTCTWrapper.nuspec -Symbols -BasePath packages
