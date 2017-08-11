# FastImageUpload
Great app for uploading screenshot or any image to the web in just few clicks! Support many image servers for survivability

It's a fantastic solution for remote assistance, when the user wants to share his screen for any help.
FastImageUpload also support link upload (if the specific server also support it).

![Alt text](/sampleImage.png?raw=true "App screenshot")

If you liked the app and you want to contribute - here are a few todo list:
* Make a comand line tool of FastImageUpload.
* Use win32api in order to show a map of windows in the current snapshot, in order to allow the user to choose specific window.
* Add functionality similar to the "Snipping tool" of Windows.
* Support user login configuration for servers who allow login (or force login such as imageshack).
* Or any other cool idea...

For supporting more servers you need to:
* Implement the IUpload interface for the server. (you can mimic other servers' functionality)
* Implement the IServerProperties interface for the server.
* Add enum value to UploadServer enum
* Add the properties class you implemented as a new item to the _servers list in the ListOfServerProperties class - respectively enum values locations
* Add the Uploader class you implemented as a new case in the StartUpload method under UploaderFactory class.

If you just want to use it, download it from here: https://github.com/supermathbond/FastImageUpload/releases/download/FastImageUpload1.0.3/Image.Uploader.exe

This code has a free license - you are free to use the code at any place you want, commercial or personal. Just put a credit for the repository or the author whenever you use it.
