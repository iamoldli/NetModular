cd ./src/UI/nm-module-blog
npm install
npm run build
cd ../../WebHost
dotnet build -r linux-x64 -c Release
dotnet publish -r linux-x64 -o ../../publish -c Release