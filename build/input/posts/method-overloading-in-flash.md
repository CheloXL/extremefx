Title: Method overloading in flash
Published: 2009-03-27
Tags: [Flash, Code, Example]
Lead: As you may already know, it is not possible to have the same method with different signatures in flash (also known as method overloading). For example, on c# you may have the same function that does different things depending on the argument count …
---
As you may already know, it is not possible to have the same method with different signatures in flash (also known as method overloading).

For example, on c# you may have the same function that does different things depending on the argument count/arguments types. The only limitation is that you can not have a method with the same signature (and different return type, for example).

So, these are perfectly valid method declarations:

```csharp
class Datetools {
	public function minus (date:Date):Timespan { ... }
	public function minus (span:Timespan):Date { ... }
}
```

And the compiler then chooses the right one based on usage.

Now, how to do this in actionscript? The most used method is to add a switch/if statement inside the function and try to match the parameters. It is not only cumbersome but also hard to maintain.

So, using a little of black magic here and there, I present here my method overloader class.

```actionscript
package extremefx.tools {
	/**
	 * @author Marcelo Volmaro
	 */
	 public final class Overloader {
	 	private var _methods:Object;

	 	public function Overloader(pTarget:Object, pFnc:String) {
	 		_methods = {};
	 		pTarget[pFnc] = _exec;
	 	}

	 	public function overload(pFunction:Function, ...args):void {
	 		_methods[args.join(")] = new Fnc(pFunction, args);
		}

		public function unoverload(...args):Boolean {
			return delete _methods[args.join(")];
		}

		private function _exec(...args):* {
			var l:int = args.length, h:String = ", theF:Fnc;

			for(var i:int = 0; i < l;i++){
				if (args[i] == null) continue;
				h += args[i].constructor;
			}

			//an exact match was found, use this.
			if ((theF = _methods[h])) return theF.f.apply(null, args);

			var bestMatch:int = -1,
			fnc:Function = null;

			for each(theF in _methods){
				if (theF.l != l) continue;//match number of arguments
				var m:int = 0;
				for (var j:uint = 0; j < l; j++){
					if (args[j] is theF.a[j]) m++;//find out which method has the best match
				}

				if (m > bestMatch){
					bestMatch = m;
					fnc = theF.f;
				}
			}

			return (fnc == null) ? null : fnc.apply(null, args);
		}
	}
}

final class Fnc {
	public var f:Function;
	public var a:Array;
	public var l:uint;
	public function Fnc(pF:Function, args:Array){
		f = pF;
		a = args;
		l = args.length;
	}
}
```

## How to use?

Simple. You need to instantiate a new Overloader class, passing as arguments the target object and the name of the function you want to overload. The overloaded function must be defined as a variable of type *Function*. The Overloader class will assign then the method that will check for arguments and calls the appropiate function.

```actionscript
public function main():void {
	var o:Overloader = new Overloader(this, "testFnc");
	o.overload(_testO1, String);
	o.overload(_testO2, String, Boolean);
	o.overload(_testO3, Number);
	o.overload(_testO4, Function, Number);
	o.overload(_testO5, DummyTest, Number);

	trace(testFnc("A String"));//A String
	trace(testFnc("A String", true));//A String:true
	trace(testFnc("A String", false));//A String:false
	trace(testFnc(123.456));//123.456
	trace(testFnc(_testO1, 123.456));//Function plus:123.456
	trace(testFnc(this, 123.456));//Class [object DummyTest] plus:123.456
}

private function _testO1(pString:String):String {
	return pString;
}

private function _testO2(pString:String, pBool:Boolean):String {
	return pString + ":"+ pBool;
}

private function _testO3(pNumber:Number):Number {
	return pNumber;
}

private function _testO4(pFunction:Function, pNumber:Number):String {
	return "Function plus:"+pNumber;
}

private function _testO5(pClass:DummyTest, pNumber:Number):String {
	return "Class "+pClass+" plus:"+pNumber;
}

public var testFnc:Function;
```

I'd tried to make it bulletproof. If the specified parameter combination does not exist on the registered function list, the overloader will try to find out the best match from all the available functions, or at least one of the available functions. Please note that since flash player does runtime checking on the argument types, an exception may be thrown if the player can not do
a type convert on the arguments (int, uint and numbers are convertibles, for example).

Hope you like it and as always, comments are welcomed!.
