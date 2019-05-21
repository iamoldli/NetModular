cd ./src/UI/nm-module-blog
npm install
npm run build
cd ../../WebHost
dotnet build -c Release
dotnet publish  -o ../../publish -c Release