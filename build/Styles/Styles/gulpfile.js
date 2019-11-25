const gulp = require("gulp");
const gulpCleanCss = require("gulp-clean-css");
const gulpLess = require("gulp-less");

const assetsRoot = "../../input/assets/styles/";

const siteCss = [
	"assets/site.less"
];

function cssSiteTask() {
	return gulp.src(siteCss, { sourcemaps: true })
		.pipe(gulpLess())
		.pipe(gulpCleanCss())
		.pipe(gulp.dest(assetsRoot, { sourcemaps: "." }));
}

function watch() {
	gulp.watch("assets/**/*.less", cssSiteTask);
}

exports.css = cssSiteTask;
exports.watch = watch;
exports.default = gulp.series(cssSiteTask);
