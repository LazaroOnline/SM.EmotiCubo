
# SM EmotiCubo

This project was done by me in my free time, as part of a Research & Development project initiative from [Ediciones SM](https://www.grupo-sm.com).  

## Experiment
The project consist of a new way for school teachers or psychologist to interact with students/patients under ages of 4-6 years old.  
Using a dice-cube that has a drawing of a face reflecting an individual emotion in each face, 
the teacher/psychologist hands over the cube to the student so the kid can put the cube in the position corresponding to the emotion that he relates to.  
It is a subtle way for kids to express their feelings without needing words to overcome shy personalities and make communication easier and fun.


## Technology
The cube is a physical IoT connected device that sends over WiFi the selected emotion (face looking up) to a backend server when a button is pressed by the teacher.  
Then the http request is registered by this repository's back-end API and stored into a Mongo DB.

This repository serves 2 purposes:
- Backend API for the IoT connected cube.
- Web page for teachers to view and manage their students emotions, see statistics and possibly anonymized results when working with groups.
