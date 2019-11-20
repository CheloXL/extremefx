Title: Flash TextArea - Standards compliant XHTML flash editor
Published: 2006-02-04
Tags: Flash, Javascript, Code
Lead: FlashTA is a WYSIWYG replacement text area for html forms. While there are various implementations done in javascript, most of them don´t work in this/that browser/os combo, and every time a browser vendor makes a change, they need to update the editor so it doesn´t break on every possible combination of browsers.
---
## Introduction

FlashTA is a WYSIWYG replacement text area for html forms. While there are various implementations done in javascript, most of them don't work in this/that browser/OS combo, and every time a browser vendor makes a change, they need to update the editor so it doesn't break on every possible combination of browsers.

FlashTA tries to overcome that problem by using flash. It also tries to adhere stricty to the xhtml specification. No presentation code is generated (so say bye bye to your idea of selecting text color, font face, etc).

FlashTA features an easy integration and multiple browser support (Mozilla, MSIE, FireFox, Opera & Safari).

## Download

Download the latest version [FlashTA.zip (176.70 kb)](/assets/files/FlashTA.zip).

## How to use

FlashTA comes with 4 files:

* FlashTA.swf (the editor itself)
* FlashFB.swf (the external file browser)
* fTAR.js (the javascript used to replace text areas with the flash editor)
* fileManager.php (example script used for image & link management)

Copy that files where your html files resides (or any other folder, it doesn't matter). You must call the fTAR.js script on any page that will be replacing text areas. The following line in the `<head>` should do the trick:

```javascript
<script type="text/javascript" src="fTAR.js"></script>
```

To tell the script what textareas you want to be replaced, add to them the class “efx_flashtextarea”:

```html
<textarea name="nameOfThisField" class="efx_flashtextarea"></textarea>
```

### Configuration

The editor configuration is handled on the javascript file. For this, you should modify the lines that read:

```javascript
fTAR.FlashTextArea.setFilemanager('fileManager.php');
fTAR.FlashTextArea.setServerURL('http://localhost');
fTAR.FlashTextArea.useFileBrowser(true);
fTAR.FlashTextArea.setBasePath('');
fTAR.FlashTextArea.setLngFile('lang/en.xml');
fTAR.FlashTextArea.setup('FlashTA.swf', 'FlashFB.swf');
```

#### Explanation:

**fTAR.setup(editor, filebrowser);**

* **editor:** path/name of the editor flash file. If you stored the editor on a folder called `flashEditor`, and the page that will use it is on the same level as the folder, the first parameter should be changed like: `flashEditor/FlashTA.swf`.
* **filebrowser:** path/name of the filebrowser flash file. If you stored the browser on a folder called `flashEditor`, and the page that will use it is on the same level as the folder, the first parameter should be changed like: `flashEditor/FlashFB.swf`.

It lets you configure an input field to browse the server for a file. It's like the normal browse button, but for server side files.

You should add the class `efx_browser` to the input field to enable it to browse all files, or with the class `efx_browser_images` to enable the field to browse for images only. Of course, for this you need a working `fileManager.php` already configured.

**fTAR.FlashTextArea.setFilemanager(path to the fileManager.php file);**: This file is used by the file browser and the image browser. Please note that this file is used for file browsing and file uploading.

**fTAR.FlashTextArea.setServerURL(serverURL);**: Url of the server used to retrieve the images. Directories are allowed here.

**fTAR.FlashTextArea.setBasePath(relativePath);**: Used to prefix links & images. The final URL of an image/link will be “relativePath/User selected Path/User selected file”.

*serverURL/relativePath/imagename* should point to a valid image. The editor will use that information to retrieve the image from the server.

You will also need to setup the fileManager.php script as following:

* **diskPath:** Folder on the server that holds the images/files accessible by the editor.
* **thumbPath:** Folder on the server, with write permissions, in where the script will store thumbnails of the user selected images, if the size of the image exceeds 210 &times; 210 px. This is to speed up the preview on the filebrowser and is not a must. If you don't want thumbs to be created, simply put an empty string ("").
* **ftpPath, ftpServer, ftpUser, ftpPass:** If PHP is running in safe mode, a directory created through mkdir()	will not be assigned to you, but to the user that your host's server or php process is running under, usually 'nobody', 'apache' or 'httpd'. In practice, this means that you can create directories, even add files to them, but you can't delete the directory or its contents nor change permissions. This information is used in that case, so the created directory can be accessed freely like any other. ftpPath needs to has the address to the starting folder in where the files/images will be stored. ftpServer, ftpUser, ftpPass need to be completed with the server, username and password used to access the website via FTP.

**fTAR.FlashTextArea.useFileBrowser(true | false);**
If you don't want the filebrowser to be accesible from withing the editor, simply pass false to this function.

**fTAR.FlashTextArea.setLngFile(path to language file);**
Both the fileBrowser and the editor are localizables through an XML file. If you want to use the editor/browser in your language, take a look at the files in the `lang` folder.

The buttons in the editor can be configured as following:

* fTAR.FlashTextAreaButtons.bold(true | false);
* fTAR.FlashTextAreaButtons.italic(true | false);
* fTAR.FlashTextAreaButtons.underline(true | false);
* fTAR.FlashTextAreaButtons.leftAlign(true | false);
* fTAR.FlashTextAreaButtons.centerAlign(true | false);
* fTAR.FlashTextAreaButtons.rightAlign(true | false);
* fTAR.FlashTextAreaButtons.justifyAlign(true | false);
* fTAR.FlashTextAreaButtons.header1(true | false);
* fTAR.FlashTextAreaButtons.header2(true | false);
* fTAR.FlashTextAreaButtons.header3(true | false);
* fTAR.FlashTextAreaButtons.header4(true | false);
* fTAR.FlashTextAreaButtons.header5(true | false);
* fTAR.FlashTextAreaButtons.header6(true | false);
* fTAR.FlashTextAreaButtons.bullets(true | false);
* fTAR.FlashTextAreaButtons.quote(true | false);
* fTAR.FlashTextAreaButtons.links(true | false);
* fTAR.FlashTextAreaButtons.images(true | false);
* fTAR.FlashTextAreaButtons.undo(true | false);
* fTAR.FlashTextAreaButtons.redo(true | false);

Also, there are some methods that can be usefull:

**fTAR.FlashTextArea.updateContent();**
Forces an update of all text areas. Usually, the content of the *browser* textareas is not updated until the user press the submit button.

If you placed the FlashTextArea inside a tabbed UI, you must call this method prior to change the text area visibility. Browsers remove the flash object from the DOM when changing visibility and you will lost all the changes made.

**fTAR.FlashTextArea.getChangedAreas();**
Returns `false` if no textareas were changed, or an array of the changed textareas.

**fTAR.FlashTextArea.hasChanged(areaName);**
Returns `true` if the area named *areaName* was changed, `false` if not.

There are also a bunch of CSS styles that you can play with. Take a look at the source code.

## Known bugs and limitations:

* Sometimes in the editor, after you insert an image, the cursor changes to a small caret, and all text inserted at that place will look like a bunch of dots. This is a known issue of the player (at least it's known by my&hellip; bug already reported to Macromedia). The text is in there and if you submit the data, the text will be in their place.

### History (* fixed/changed, + added, &ndash; removed)

* \+ The editor is now localizable.
* \+ Redo / Undo.
* \+ Copy as HTML. (Right click over the text).
* \+ Added a way to determine what textareas were changed.
* \- Flash automatic update if flash version < 8.
* \* Finally!, discovered a way to fix Flash's ExternalInterface on IE under forms. This simplified a lot the code.

---

* \*  Changed how the editor gets the text. Now it can be placed on “tabs” systems without loosing the content. You need to call fTAR.FlashTextArea.updateContent() before doing anything with the editor visibility.
* \*  Fixed a lot of image duplication bugs caused by the Flash Player.
* \+ Added the ability to delete files on the file browser.

---

* \*  Ported to use Macromedia Components
* \+ Added backgroundColor
* \*  Fixed image dissapearing bug.
* \+ Re-added the scrollbar.
* \*  A lot of bugfixes that i don't remember now.
---

* \+ Added a “delete” button in the insert image window.
* \* Buttons config now works as expected.
* \*  Fixed some problems with unicode chars under Opera 8.5+
* \- Removed the scrollbar &ndash; Now the editor resizes itself as needed.

---

* \+ First launch

See the complete project at [OSFlash/FlashTextArea](http://osflash.org/flashtextarea).
