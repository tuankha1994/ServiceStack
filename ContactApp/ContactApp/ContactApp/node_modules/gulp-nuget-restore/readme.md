# gulp-nuget-restore [![Build Status](https://travis-ci.org/tparnell8/gulp-nuget-restore.svg?branch=master)](https://travis-ci.org/tparnell8/gulp-nuget-restore)

> This is  simple gulp plugin to restore nuget packages


## Install

```
$ npm install --save-dev gulp-nuget-restore
```


## Usage

```js
var gulp = require('gulp');
var nugetRestore = require('gulp-nuget-restore');

gulp.task('default', function () {
	return gulp.src('./path/to/MySlnFile.sln')
		.pipe(nugetRestore());
});
```

Seems work well well with [gulp-msbuild](https://github.com/hoffi/gulp-msbuild)

```js

var gulp = require('gulp');
var nugetRestore = require('gulp-nuget-restore');
var msbuild = require("gulp-msbuild");

gulp.task('build', function () {
    return gulp.src('WebApplication9.sln')
        .pipe(nugetRestore())
        .pipe(msbuild({
            targets: ['Clean', 'Build'],
            toolsVersion: 14.0}
          ));
});


```


## API

### nugetRestore(options)

#### options

##### nugetPath

Type: `string`  
Default: `__dir + ./nuget.exe`

You can provide a custom path to the nuget executable. One is bundled in the module so this is optional if you want a specific version

##### monoPath

Type: `string`  
Default: `null`

You can provide a path to mono if you are on ubuntu (currently untested)


#### additionalArgs

type `array`
Default: `null`

You can provide additional arguments such as -PackageDirectory

```js

var gulp = require('gulp');
var nugetRestore = require('gulp-nuget-restore');

gulp.task('default', function () {
	return gulp.src('./path/to/MySlnFile.sln')
		.pipe(nugetRestore({additionalArgs: ["-PackagesDirectory", "..\\packages"]}));
});

```


## License

MIT © [Tommy Parnell](https://github.com/tparnell8)
