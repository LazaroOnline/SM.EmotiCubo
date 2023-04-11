
# SM EmotiCubo

This project was done by me (Lazaro) in my free time, as part of a Research & Development project initiative from [Ediciones SM](https://www.grupo-sm.com).  

## Experiment
The project consist of a new way for school teachers or psychologist to interact with students/patients under ages of 4-6 years old.  
Using a dice-cube that has a drawing of a face reflecting an individual emotion in each face, 
the teacher/psychologist hands over the cube to the student so the kid can put the cube in the position corresponding to the emotion that he relates to.  
It is a subtle way for kids to express their feelings without needing words to overcome shy personalities and make communication easier and fun.


## Technology
The cube is a physical IoT connected device that sends over WiFi the selected emotion (face looking up) to a back-end server when a button is pressed by the teacher.  
Then the HTTP request is registered by this repository's back-end API and stored into a MongoDB.

This repository serves 2 purposes:
- BackEnd API for the IoT connected cube.
- Web page for teachers to view and manage their students emotions, see statistics and possibly anonymized results when working with groups.


## Requirements
- [DotNet SDK](https://dotnet.microsoft.com/download) v6
- [NodeJs](https://nodejs.org/) (tested with v19)
- [MongoDB](https://www.mongodb.com/try/download/community) v6
- [Docker](https://docs.docker.com/desktop/install/windows-install/) Engine 17.09.0+

## How to run
Run the docker-compose file, enter the login page and add some data from the browser's developer tool's console with this js:
```js
 fetch("/api/Session/Save/3");
 fetch("/api/Session/Save/3");
 fetch("/api/Session/Save/2");
 fetch("/api/Session/Save/1");
 fetch("/api/Session/Save/1");
 fetch("/api/Session/Save/1");
```

## Update npm packages
```
npm i @progress/kendo-ui@latest
npm i bootstrap@latest
npm i jquery@latest
npm i knockout@latest
npm i knockout-kendo@latest
npm i -D gulp@latest
npm i -D gulp-concat@latest
npm i -D gulp-uglify@latest
npm i -D gulp-sourcemaps@latest
npm i -D del@latest
npm i -D run-sequence@latest
```