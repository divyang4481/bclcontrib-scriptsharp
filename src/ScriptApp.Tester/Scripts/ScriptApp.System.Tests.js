// ScriptApp.System.Tests.js
(function(){function executeScript(){
window.ByteBuilderTests=function(){}
window.MathMatrixTests=function(){}
MathMatrixTests.prototype={testMultiplyMM:function(){var $0=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];var $1=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];var $2=new Array(16);SystemEx.MathMatrix.multiplyMM($2,0,$0,0,$1,0);window.assertEquals([0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],$2);}}
window.Math3DTests=function(){}
Type.registerNamespace('IO');IO.StreamTests=function(){}
IO.SETests=function(){}
IO.PathTests=function(){}
IO.MemoryStreamTests=function(){}
IO.FileStreamTests=function(){}
IO.FileInfoTests=function(){}
IO.FileTests=function(){}
IO.DirectoryTests=function(){}
Type.registerNamespace('Security.Cryptography');Security.Cryptography.Md4SlimTests=function(){}
Security.Cryptography.CrcSlimTests=function(){}
Type.registerNamespace('Html');Html.Bar=function(){}
Html.Foo=function(bar){this.$0=bar;}
Html.Foo.prototype={$0:null,getBar:function(){return this.$0;}}
Html.Rabbit=function(weight){this.$0=weight;}
Html.Rabbit.prototype={$0:0,getWeight:function(){return this.$0;}}
Html.JsInjectTests=function(){}
Html.JsInjectTests.prototype={testInstantiation:function(){var $0=new JsInject.ContainerBuilder();$0.Register('bar',ss.Delegate.create(this,function($p1_0){
return new Html.Bar();}));$0.Register('foo',ss.Delegate.create(this,function($p1_0){
return new Html.Foo($p1_0.Resolve('bar'));}));var $1=$0.Create();var $2=$1.Resolve('foo');window.assertNotNull($2);window.assertNotNull($2.getBar());},testInstanceIsReusedWithinContainer:function(){var $0=new JsInject.ContainerBuilder();$0.Register('bar',ss.Delegate.create(this,function($p1_0){
return new Html.Bar();})).Reused();var $1=$0.Create();var $2=$1.Resolve('bar');var $3=$1.Resolve('bar');window.assertSame($2,$3);},testResolveWithParameter:function(){var $0=new JsInject.ContainerBuilder();$0.Register('Rabbit',ss.Delegate.create(this,function($p1_0,$p1_1){
return new Html.Rabbit($p1_1);}));var $1=$0.Create();var $2=$1.Resolve('Rabbit',55);window.assertNotNull($2);window.assertEquals(55,$2.getWeight());}}
Html.LocalStorageTests=function(){}
Html.LocalStorageTests.prototype={tearDown:function(){while(SystemEx.Html.LocalStorage.get_length()>0){SystemEx.Html.LocalStorage.removeItem(SystemEx.Html.LocalStorage.key(0));}},testGetItem_fails_for_missing_item:function(){},testGetItem_passes_when_set_item_requested:function(){SystemEx.Html.LocalStorage.setItem('Key','Value');window.assertEquals('Value',SystemEx.Html.LocalStorage.getItem('Key'));},testKey_fails_for_outofbounds:function(){window.assertException(ss.Delegate.create(this,function(){
SystemEx.Html.LocalStorage.key(0);}),'Error');},testKey_passes_when_set_item_key_requested:function(){SystemEx.Html.LocalStorage.setItem('Key','Value');window.assertEquals('Key',SystemEx.Html.LocalStorage.key(0));},testLenght_passes_when_set_single_item_returns_one:function(){SystemEx.Html.LocalStorage.setItem('Key','Value');window.assertEquals(1,SystemEx.Html.LocalStorage.get_length());},testRemoveItem_fails_when_item_doenst_exist:function(){},testRemoveItem_passes_when_item_exists_and_items_removed:function(){SystemEx.Html.LocalStorage.setItem('Key','Value');SystemEx.Html.LocalStorage.removeItem('Key');window.assertEquals(0,SystemEx.Html.LocalStorage.get_length());},testSetItem_fails_when_set_item_exists:function(){},testSetItem_passes_when_set_item_exists:function(){SystemEx.Html.LocalStorage.setItem('Key','Value');window.assertEquals('Value',SystemEx.Html.LocalStorage.getItem('Key'));},testSetItem_passes_when_set_item_duplicated:function(){SystemEx.Html.LocalStorage.setItem('Key','Value');SystemEx.Html.LocalStorage.setItem('Key','Value2');window.assertEquals('Value2',SystemEx.Html.LocalStorage.getItem('Key'));}}
Type.registerNamespace('Text');Text.PackedStringTests=function(){}
Type.registerNamespace('Interop');Interop.CSyntaxTests=function(){}
Interop.CSyntaxTests.prototype={test_simple_format:function(){window.assertEquals('format',SystemEx.Interop.CSyntax.sprintf('format'));},test_single_decimal_format:function(){window.assertEquals('test \u0000',SystemEx.Interop.CSyntax.sprintf('test %d',[1]));},testSprintfInt32_passes:function(){window.assertEquals('test \u0000',SystemEx.Interop.CSyntax.sprintfInt32('test %d',1));}}
ByteBuilderTests.registerClass('ByteBuilderTests');MathMatrixTests.registerClass('MathMatrixTests');Math3DTests.registerClass('Math3DTests');IO.StreamTests.registerClass('IO.StreamTests');IO.SETests.registerClass('IO.SETests');IO.PathTests.registerClass('IO.PathTests');IO.MemoryStreamTests.registerClass('IO.MemoryStreamTests');IO.FileStreamTests.registerClass('IO.FileStreamTests');IO.FileInfoTests.registerClass('IO.FileInfoTests');IO.FileTests.registerClass('IO.FileTests');IO.DirectoryTests.registerClass('IO.DirectoryTests');Security.Cryptography.Md4SlimTests.registerClass('Security.Cryptography.Md4SlimTests');Security.Cryptography.CrcSlimTests.registerClass('Security.Cryptography.CrcSlimTests');Html.Bar.registerClass('Html.Bar');Html.Foo.registerClass('Html.Foo');Html.Rabbit.registerClass('Html.Rabbit');Html.JsInjectTests.registerClass('Html.JsInjectTests');Html.LocalStorageTests.registerClass('Html.LocalStorageTests');Text.PackedStringTests.registerClass('Text.PackedStringTests');Interop.CSyntaxTests.registerClass('Interop.CSyntaxTests');(function(){window.TestCase('ByteBuilderTests',ByteBuilderTests.prototype);})();
(function(){window.TestCase('MathMatrixTests',MathMatrixTests.prototype);})();
(function(){window.TestCase('Math3DTests',Math3DTests.prototype);})();
(function(){window.TestCase('StreamTests',IO.StreamTests.prototype);})();
(function(){window.TestCase('SETests',IO.SETests.prototype);})();
(function(){window.TestCase('PathTests',IO.PathTests.prototype);})();
(function(){window.TestCase('MemoryStreamTests',IO.MemoryStreamTests.prototype);})();
(function(){window.TestCase('FileStreamTests',IO.FileStreamTests.prototype);})();
(function(){window.TestCase('FileInfoTests',IO.FileInfoTests.prototype);})();
(function(){window.TestCase('FileTests',IO.FileTests.prototype);})();
(function(){window.TestCase('DirectoryTests',IO.DirectoryTests.prototype);})();
(function(){window.TestCase('Md4SlimTests',Security.Cryptography.Md4SlimTests.prototype);})();
(function(){window.TestCase('CrcSlimTests',Security.Cryptography.CrcSlimTests.prototype);})();
(function(){window.TestCase('JsInjectTests',Html.JsInjectTests.prototype);})();
(function(){window.TestCase('LocalStorage',Html.LocalStorageTests.prototype);})();
(function(){window.TestCase('PackedStringTests',Text.PackedStringTests.prototype);})();
(function(){window.TestCase('CSyntaxTests',Interop.CSyntaxTests.prototype);})();
}
ss.loader.registerScript('ScriptApp.System.Tests',['Script.WebEx'],executeScript);})();// This script was generated using Script# v0.6.3.0
