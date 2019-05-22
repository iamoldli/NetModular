cd ./src/UI/td-module-archives-manager
cd ../../WebHost.Electron
dotnet build -r win-x64 -c Release
dotnet publish -r win-x64 -c Release
electronize build /target win
cd ./obj/desktop/win
electron-packager . --platform=win32 --arch=x64  --out="../../../bin/desktop" --overwrite --icon="./bin/wwwroot/images/favicon.ico"
pause