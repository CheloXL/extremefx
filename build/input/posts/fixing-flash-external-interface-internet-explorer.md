Title: Fixing Flash External Interface inside Forms on Internet Explorer
Published: 2007-02-23
Tags: [Flash, Javascript]
Lead: "Tried to use External Interface on a flash object inside an HTML form in Internet Explorer without any luck?. You know you actually can... right? No?. Follow this steps to fix that nasty EI under IE :)"
---
As you all may know (or may not), if you want to use External Interface on a flash object that lives inside an "HTML" form, it will not work.

I will show you a simple test:

```html
<form>
	<object id="flashElement1" width="256" height="64" data="files/bug4.swf" type="application/x-shockwave-flash">
		<param name="movie" value="files/bug4.swf" />
		<param name="allowScriptAccess" value="always" />
	</object>
	<p><a onclick="document.getElementById('flashElement1').testFunction(); return false;" href="#">Click to test</a></p>
</form>
```

First of all, you will get a javascript error saying that the element `flashElement1` is undefined. And while in the flash window you will see the *CallBack added Ok*, if you click on the *Click to test* link you will get another error saying that the `Object doesn't support that property or method`.

**Why?:** Easy. The way the javascript bridge with flash was coded is really awful. In IE, every time you add an `ID` attribute to an `HTML` element, that element is added to the `window` element so you can access it directly using `window.elementID...` but if you place the element inside an `HTML` form, the element is added to the form element (`document.forms[0].elementID` if you have only one form) and not window element. I suppose the person who did the bridge knowns nothing about this... I will not get into the details here, but you can believe me: The bridge it's screaming for a rewriting.

**How?:** Well, this will depend on how are you embedding the object on the `HTML` page. If you use [SwfObject](http://blog.deconcept.com/swfobject/) you should add the following line after the line 105 (the line that reads `n.innerHTML = this.getSWFHTML();`):

```javascript
if(!(navigator.plugins && navigator.mimeTypes.length))
    window[this.getAttribute('id')] = document.getElementById(this.getAttribute('id'));
```

That applies to the current version of SwfObject (1.4.4).

If you use [UFO](http://www.bobbyvandersluis.com/ufo/) you should add the following line after the line 230 (the line that reads `_e.innerHTML = '<object classid="...`):

```javascript
if (_fo["id"]) window[_fo["id"]] = document.getElementById(_fo["id"]);
```

That applies to UFO 3.20.

And if you use [Adobe's Active Content](http://www.adobe.com/devnet/activecontent/articles/devletter.html) you should add the following line after the line 24 (the line that reads `document.write(str);`):

```javascript
if (objAttrs["id"]) window[objAttrs["id"]] = document.getElementById(objAttrs["id"]);
```
