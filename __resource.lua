resource_manifest_version '05cfa83c-a124-4cfa-a768-c24a5811d8f9'
description 'IgiCore Framework'
version '0.0.1'

server_scripts {
	'IgiCore.Server.net.dll',
}

client_scripts {
	'Plugins/roleplay/Roleplay.Core.net.dll',
	'Plugins/roleplay/Roleplay.Client.net.dll',

	'Plugins/banking/Banking.Core.net.dll',
	'Plugins/banking/Banking.Client.net.dll',

	'IgiCore.Client.net.dll',
}

files {
	'Newtonsoft.Json.dll',
	'EntityFramework.dll',
	'System.ComponentModel.DataAnnotations.dll',

	'IgiCore.Models.net.dll',
	'IgiCore.SDK.Core.net.dll',
	'IgiCore.SDK.Client.net.dll',

	'Interface/loading-screen/index.html',
	'Interface/loading-screen/main.js',
	'Interface/loading-screen/main.css',
	'Interface/loading-screen/libs/jquery.min.js',
	'Interface/loading-screen/libs/bootstrap.min.css',
	'Interface/loading-screen/img/logo.png',
	'Interface/loading-screen/img/bg1.jpg',
	'Interface/loading-screen/img/bg2.jpg',
	'Interface/loading-screen/img/bg3.jpg',
	'Interface/loading-screen/img/bg4.jpg',
	'Interface/loading-screen/img/bg5.jpg',

	'Interface/dist/index.html',
	'Interface/dist/bundle.js',
	'Interface/dist/gravity.woff2',
	'Interface/dist/pricedown.woff2',
	'Interface/dist/male.png',
}

loadscreen 'Interface/loading-screen/index.html'
ui_page 'Interface/dist/index.html'
