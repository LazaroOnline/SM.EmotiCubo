module.exports = function () {
	var config = {
		src: {
			js: ['node_modules/**/*.js', '!**/*Spec.js']
			,css: ['']
		},
		dest: {
			root: "build"
			,js:  "build/js"
			,css: "build/css"
			,img: "build/img"
			,files: {
				allminjs: 'all.min.js'
			}
		}



		,"globs": {
			"js": {
				"src": ["./Scripts/**/*.js", "!./Scripts/Vendor/**", "!./Scripts/Core/testing/**", "!**/tests/**", "!**/*Fake.js", "!**/*Stub.js", "!**/*Spec.js"],
				"toMinify": "./cdn/Scripts/**/*.js",
				"min": "./Scripts/**/min"
			},
			"ts": {
				"src": "./Scripts/**/*.ts"
			},
			"html": {
				"src": ["./Scripts/**/*.html", "!**/*Chutzpah*.html"]
			},
			"less": {
				"src": "./cdn/Content/less/*.less"
			}
		},
		"tasks": {
			"build-js": []
		}
	};
    return config;
};
