# XmlHelper
Written by Jason Yap

## How to run
This has been prepared and tested on a Windows machine. It is also recommended to use such and similar environment.
To compile, you may open this project on Visual Studio 2022 thru the solution file named BoostDraft.DetermineXml.sln
Press Build (Ctrl + Shift + B), then Run by pressing Start Without Debugging (Ctrl + F5)

You may also execute this on the command line. On the root directory, compile by typing dotnet build.
Then browse thru this subpath \BoostDraft.DetermineXml\bin\Debug\net6.0, and you should be able to find a generated
executable file named BoostDraft.XmlUtils.exe. 

This console app expects an XML string argument from the command line. Example : ```BoostDraft.XmlUtils.exe "<Design><Code>hello world</Code></Design>"```
If nothing is provided, a default XML string ```"<Design><Code>hello world</Code></Design>"``` is provided for demo purposes.

## Design and assumptions

A function under BoostDraft.XmlUtils.XmlHelper named DetermineXml is provided, which takes in a string and returns a boolean result on whether the input string is a valid XML.
The function utilizes an internal stack to enforce the rules of a valid XML string:
- Each starting element must have a corresponding ending element
- Nesting of elements within each other must be well nested, which means start first must end last. For example, ```<tutorial><topic>XML</topic></tutorial>``` is a correct way of nesting but ```<tutorial><topic>XML</tutorial></topic>``` is not

The stack checks whether each starting element that is pushed, will have a corresponding ending element that is eventually popped as it traverses the entire input string.
And because the stack behaves in a last in first out manner, the more nested elements are pushed, naturally, it will expect that those elements will be popped in reverse.


Some terminologies that I have used in my code:
- A tag refers to a string that is enclosed in a tag start ```<``` and a tag end ```>```. 
- Element is synonymously a tag, but denotes position as tags come in pairs. A start element ```<Design>``` and end element ```<\Design>```. 
The end element is the tag with a ```\```
- Elements in an XML string belong in a hierarchy, and these are pushed and popped in the same nested manner


Anything that is not enclosed in a tag are simply treated as string content, as long as they are within one pair of elements.

Lastly, as I am navigating thru each substring and char index operations in the input, I chose to use and identify them using the new ```Span<char>``` because of its performance benefit without memory allocations, which will be efficient for large input strings.
Unfortunately, it doesnt fit yet as a type parameter for the generic ```Stack<T>``` so I had settled for ```Stack<string>```

## Unit tests
I have selected NUnit framework and the tests I have written can be found under BoostDraft.Tests