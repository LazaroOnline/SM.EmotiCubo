﻿const gulp = require('gulp');
const concat = require('gulp-concat');
const uglify = require('gulp-uglify');
const sourcemaps = require('gulp-sourcemaps');
const del = require('del');
const config = require('./gulp-config.js')();


// gulp.task('default', ['html', 'css', 'js']);
gulp.task('default', function () {
    // place code for your default task here
	console.log('This is the default gulp task, runs with "gulp" command with no params.');
});
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
gulp.task('clean', function () {
	console.log('cleaning folder: ' + config.dest.root);
	return del(config.dest.root);
});

gulp.task('clean-js', function () {
	console.log('cleaning folder: ' + config.dest.js);
	return del(config.dest.js);
});

gulp.task('clean-css', function () {
	console.log('cleaning folder: ' + config.dest.css);
	return del(config.dest.css);
});
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
const runSequence = require('run-sequence');
gulp.task('build-js', ['clean-js'], function () {
	// Minify and copy all JavaScript (except vendor scripts) 
	// with sourcemaps all the way down 

	//runSequence('ts-compile', 'copy', done);
	console.log('building: ' + config.src.js);
	return gulp.src(config.src.js)
	//	.pipe(sourcemaps.init())
		//.pipe(coffee())
	//	.pipe(uglify())
		.pipe(concat(config.dest.files.allminjs))
	//	.pipe(sourcemaps.write())
	//	.pipe(plugins.chmod(666))
		.pipe(gulp.dest(config.dest.js)); // 'build/js'
});

const FILTRO_TODOS_ARCHIVOS = '/**/*.*';
const NODE_MODULES_PATH = './node_modules';
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

gulp.task('watch', ['watch-js', 'watch-css']);

gulp.task('watch-js', function () {
	gulp.watch(config.src.js, ['build-js']);
});

gulp.task('watch-css', function () {
	gulp.watch(config.src.css, ['build-css']);
});
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
