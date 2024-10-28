WEB_PATH=./web
WEB_PORT=5072
start:
	cd $(API_PATH) && \
	nohup dotnet Web.dll --urls=http://0.0.0.0:$(API_PORT) > web.log 2>&1 & \
	echo "ASP.NET Web start on port $(API_PORT)"

stop:
	pkill -f "dotnet Web.dll" || true
	echo "Web application stopped"
