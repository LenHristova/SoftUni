________________________________________________________________________
																		
			ALL SUPPORTED COMMANDS										
________________________________________________________________________					
  
  Displays information about all of the supported commands
  ENTER help
  
________________________________________________________________________
  																		
  		SUPPORTED COMMANDS' REQUIREMENTS AND EXAMPLES						
________________________________________________________________________
  
  Create directory:
  ENTER "mkdir" command + directory's name
  Example: mkdir NewFolder
------------------------------------------------------------------------  
  
  Traverse directory:
  ENTER "ls" command (default depth is 0)
  Example: ls
  OR ENTER "ls" command + depth (number -> wanted depth)
  Example: ls 2
------------------------------------------------------------------------   
  
  Read database from file (if exist):
  ENTER "readdb" command + file's name
  Example: readDb Resources/data.txt
------------------------------------------------------------------------   
  
  Show info for given course (if exist)
  ENTER "show" command + course's name 
  Example: show C#_Jul_2016
  OR show info for given student (if exist) in given course (if exist)
  OR ENTER "show" command + course's name + student's username
  Example: show C#_Jul_2016 BruceReid89_9114
------------------------------------------------------------------------  
  
  Filter and take students:
  ENTER "filter" command + courseName + filter + takeCommand + takeQuantity
  Filter could be excellent/average/poor
  takeQuantity could be "all" 
  Example: filter Arduino_Mar_2016 excellent take all
  OR number:
  Example: filter Arduino_Mar_2016 average take 1
------------------------------------------------------------------------  
  
  Order and take students:
  ENTER "order" command + courseName + orderType + takeCommand + takeQuantity	
  Order criterion could be ascending/descending
  takeQuantity could be "all" 
  Example: order Arduino_Mar_2016 ascending take all
  OR number:
  Example: order Arduino_Mar_2016 descending take 3
------------------------------------------------------------------------  

  Display students/courses ascending/descending by name (if data exist):
  ENTER "display" command + wanted data to sort + wanted sort type
  Example: display courses ascending
  Example: display students descending
------------------------------------------------------------------------  
  
  Drop database:
  ENTER "dropdb" command
------------------------------------------------------------------------  
  		
  Change path relatively (if exist in current directory):
  ENTER "cdrel" command + relative path
  Example: cdrel Resources
------------------------------------------------------------------------  
  
  Open file (if exist in current directory): 
  ENTER "open" command + file's name
  Example: open data.txt
------------------------------------------------------------------------  
  
  Changes path relatively:
  ENTER "cdabs" command + absolute path
------------------------------------------------------------------------  
  
  Compare files:
  ENTER: "cmd" command + 
  		absolute path of the first file + 
  		absolute path of the second file 
------------------------------------------------------------------------  