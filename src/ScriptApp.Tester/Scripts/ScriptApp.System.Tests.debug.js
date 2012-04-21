//! ScriptApp.System.Tests.debug.js
//

(function() {
function executeScript() {

////////////////////////////////////////////////////////////////////////////////
// ByteBuilderTests

window.ByteBuilderTests = function ByteBuilderTests() {
}


////////////////////////////////////////////////////////////////////////////////
// MathMatrixTests

window.MathMatrixTests = function MathMatrixTests() {
}
MathMatrixTests.prototype = {
    
    testMultiplyMM: function MathMatrixTests$testMultiplyMM() {
        var a = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ];
        var b = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ];
        var ab = new Array(16);
        SystemEx.MathMatrix.multiplyMM(ab, 0, a, 0, b, 0);
        window.assertEquals([ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ], ab);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Math3DTests

window.Math3DTests = function Math3DTests() {
}


Type.registerNamespace('IO');

////////////////////////////////////////////////////////////////////////////////
// IO.StreamTests

IO.StreamTests = function IO_StreamTests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.SETests

IO.SETests = function IO_SETests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.PathTests

IO.PathTests = function IO_PathTests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.MemoryStreamTests

IO.MemoryStreamTests = function IO_MemoryStreamTests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.FileStreamTests

IO.FileStreamTests = function IO_FileStreamTests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.FileInfoTests

IO.FileInfoTests = function IO_FileInfoTests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.FileTests

IO.FileTests = function IO_FileTests() {
}


////////////////////////////////////////////////////////////////////////////////
// IO.DirectoryTests

IO.DirectoryTests = function IO_DirectoryTests() {
}


Type.registerNamespace('Security.Cryptography');

////////////////////////////////////////////////////////////////////////////////
// Security.Cryptography.Md4SlimTests

Security.Cryptography.Md4SlimTests = function Security_Cryptography_Md4SlimTests() {
}


////////////////////////////////////////////////////////////////////////////////
// Security.Cryptography.CrcSlimTests

Security.Cryptography.CrcSlimTests = function Security_Cryptography_CrcSlimTests() {
}


Type.registerNamespace('Html');

////////////////////////////////////////////////////////////////////////////////
// Html.Bar

Html.Bar = function Html_Bar() {
}


////////////////////////////////////////////////////////////////////////////////
// Html.Foo

Html.Foo = function Html_Foo(bar) {
    /// <param name="bar" type="Html.Bar">
    /// </param>
    /// <field name="_bar" type="Html.Bar">
    /// </field>
    this._bar = bar;
}
Html.Foo.prototype = {
    _bar: null,
    
    getBar: function Html_Foo$getBar() {
        /// <returns type="Html.Bar"></returns>
        return this._bar;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Html.Rabbit

Html.Rabbit = function Html_Rabbit(weight) {
    /// <param name="weight" type="Number" integer="true">
    /// </param>
    /// <field name="_weight" type="Number" integer="true">
    /// </field>
    this._weight = weight;
}
Html.Rabbit.prototype = {
    _weight: 0,
    
    getWeight: function Html_Rabbit$getWeight() {
        /// <returns type="Number" integer="true"></returns>
        return this._weight;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Html.JsInjectTests

Html.JsInjectTests = function Html_JsInjectTests() {
}
Html.JsInjectTests.prototype = {
    
    testInstantiation: function Html_JsInjectTests$testInstantiation() {
        var containerBuilder = new JsInject.ContainerBuilder();
        containerBuilder.Register('bar', ss.Delegate.create(this, function(c) {
            return new Html.Bar();
        }));
        containerBuilder.Register('foo', ss.Delegate.create(this, function(c) {
            return new Html.Foo(c.Resolve('bar'));
        }));
        var container = containerBuilder.Create();
        var foo = container.Resolve('foo');
        window.assertNotNull(foo);
        window.assertNotNull(foo.getBar());
    },
    
    testInstanceIsReusedWithinContainer: function Html_JsInjectTests$testInstanceIsReusedWithinContainer() {
        var containerBuilder = new JsInject.ContainerBuilder();
        containerBuilder.Register('bar', ss.Delegate.create(this, function(c) {
            return new Html.Bar();
        })).Reused();
        var container = containerBuilder.Create();
        var bar1 = container.Resolve('bar');
        var bar2 = container.Resolve('bar');
        window.assertSame(bar1, bar2);
    },
    
    testResolveWithParameter: function Html_JsInjectTests$testResolveWithParameter() {
        var containerBuilder = new JsInject.ContainerBuilder();
        containerBuilder.Register('Rabbit', ss.Delegate.create(this, function(c, weight) {
            return new Html.Rabbit(weight);
        }));
        var container = containerBuilder.Create();
        var rabbit = container.Resolve('Rabbit', 55);
        window.assertNotNull(rabbit);
        window.assertEquals(55, rabbit.getWeight());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Html.LocalStorageTests

Html.LocalStorageTests = function Html_LocalStorageTests() {
}
Html.LocalStorageTests.prototype = {
    
    tearDown: function Html_LocalStorageTests$tearDown() {
        while (SystemEx.Html.LocalStorage.get_length() > 0) {
            SystemEx.Html.LocalStorage.removeItem(SystemEx.Html.LocalStorage.key(0));
        }
    },
    
    testGetItem_fails_for_missing_item: function Html_LocalStorageTests$testGetItem_fails_for_missing_item() {
    },
    
    testGetItem_passes_when_set_item_requested: function Html_LocalStorageTests$testGetItem_passes_when_set_item_requested() {
        SystemEx.Html.LocalStorage.setItem('Key', 'Value');
        window.assertEquals('Value', SystemEx.Html.LocalStorage.getItem('Key'));
    },
    
    testKey_fails_for_outofbounds: function Html_LocalStorageTests$testKey_fails_for_outofbounds() {
        window.assertException(ss.Delegate.create(this, function() {
            SystemEx.Html.LocalStorage.key(0);
        }), 'Error');
    },
    
    testKey_passes_when_set_item_key_requested: function Html_LocalStorageTests$testKey_passes_when_set_item_key_requested() {
        SystemEx.Html.LocalStorage.setItem('Key', 'Value');
        window.assertEquals('Key', SystemEx.Html.LocalStorage.key(0));
    },
    
    testLenght_passes_when_set_single_item_returns_one: function Html_LocalStorageTests$testLenght_passes_when_set_single_item_returns_one() {
        SystemEx.Html.LocalStorage.setItem('Key', 'Value');
        window.assertEquals(1, SystemEx.Html.LocalStorage.get_length());
    },
    
    testRemoveItem_fails_when_item_doenst_exist: function Html_LocalStorageTests$testRemoveItem_fails_when_item_doenst_exist() {
    },
    
    testRemoveItem_passes_when_item_exists_and_items_removed: function Html_LocalStorageTests$testRemoveItem_passes_when_item_exists_and_items_removed() {
        SystemEx.Html.LocalStorage.setItem('Key', 'Value');
        SystemEx.Html.LocalStorage.removeItem('Key');
        window.assertEquals(0, SystemEx.Html.LocalStorage.get_length());
    },
    
    testSetItem_fails_when_set_item_exists: function Html_LocalStorageTests$testSetItem_fails_when_set_item_exists() {
    },
    
    testSetItem_passes_when_set_item_exists: function Html_LocalStorageTests$testSetItem_passes_when_set_item_exists() {
        SystemEx.Html.LocalStorage.setItem('Key', 'Value');
        window.assertEquals('Value', SystemEx.Html.LocalStorage.getItem('Key'));
    },
    
    testSetItem_passes_when_set_item_duplicated: function Html_LocalStorageTests$testSetItem_passes_when_set_item_duplicated() {
        SystemEx.Html.LocalStorage.setItem('Key', 'Value');
        SystemEx.Html.LocalStorage.setItem('Key', 'Value2');
        window.assertEquals('Value2', SystemEx.Html.LocalStorage.getItem('Key'));
    }
}


Type.registerNamespace('Text');

////////////////////////////////////////////////////////////////////////////////
// Text.PackedStringTests

Text.PackedStringTests = function Text_PackedStringTests() {
}


Type.registerNamespace('Interop');

////////////////////////////////////////////////////////////////////////////////
// Interop.CSyntaxTests

Interop.CSyntaxTests = function Interop_CSyntaxTests() {
}
Interop.CSyntaxTests.prototype = {
    
    test_simple_format: function Interop_CSyntaxTests$test_simple_format() {
        window.assertEquals('format', SystemEx.Interop.CSyntax.sprintf('format'));
    },
    
    test_single_decimal_format: function Interop_CSyntaxTests$test_single_decimal_format() {
        window.assertEquals('test \u0000', SystemEx.Interop.CSyntax.sprintf('test %d', [ 1 ]));
    },
    
    testSprintfInt32_passes: function Interop_CSyntaxTests$testSprintfInt32_passes() {
        window.assertEquals('test \u0000', SystemEx.Interop.CSyntax.sprintfInt32('test %d', 1));
    }
}


ByteBuilderTests.registerClass('ByteBuilderTests');
MathMatrixTests.registerClass('MathMatrixTests');
Math3DTests.registerClass('Math3DTests');
IO.StreamTests.registerClass('IO.StreamTests');
IO.SETests.registerClass('IO.SETests');
IO.PathTests.registerClass('IO.PathTests');
IO.MemoryStreamTests.registerClass('IO.MemoryStreamTests');
IO.FileStreamTests.registerClass('IO.FileStreamTests');
IO.FileInfoTests.registerClass('IO.FileInfoTests');
IO.FileTests.registerClass('IO.FileTests');
IO.DirectoryTests.registerClass('IO.DirectoryTests');
Security.Cryptography.Md4SlimTests.registerClass('Security.Cryptography.Md4SlimTests');
Security.Cryptography.CrcSlimTests.registerClass('Security.Cryptography.CrcSlimTests');
Html.Bar.registerClass('Html.Bar');
Html.Foo.registerClass('Html.Foo');
Html.Rabbit.registerClass('Html.Rabbit');
Html.JsInjectTests.registerClass('Html.JsInjectTests');
Html.LocalStorageTests.registerClass('Html.LocalStorageTests');
Text.PackedStringTests.registerClass('Text.PackedStringTests');
Interop.CSyntaxTests.registerClass('Interop.CSyntaxTests');
(function () {
    window.TestCase('ByteBuilderTests', ByteBuilderTests.prototype);
})();
(function () {
    window.TestCase('MathMatrixTests', MathMatrixTests.prototype);
})();
(function () {
    window.TestCase('Math3DTests', Math3DTests.prototype);
})();
(function () {
    window.TestCase('StreamTests', IO.StreamTests.prototype);
})();
(function () {
    window.TestCase('SETests', IO.SETests.prototype);
})();
(function () {
    window.TestCase('PathTests', IO.PathTests.prototype);
})();
(function () {
    window.TestCase('MemoryStreamTests', IO.MemoryStreamTests.prototype);
})();
(function () {
    window.TestCase('FileStreamTests', IO.FileStreamTests.prototype);
})();
(function () {
    window.TestCase('FileInfoTests', IO.FileInfoTests.prototype);
})();
(function () {
    window.TestCase('FileTests', IO.FileTests.prototype);
})();
(function () {
    window.TestCase('DirectoryTests', IO.DirectoryTests.prototype);
})();
(function () {
    window.TestCase('Md4SlimTests', Security.Cryptography.Md4SlimTests.prototype);
})();
(function () {
    window.TestCase('CrcSlimTests', Security.Cryptography.CrcSlimTests.prototype);
})();
(function () {
    window.TestCase('JsInjectTests', Html.JsInjectTests.prototype);
})();
(function () {
    window.TestCase('LocalStorage', Html.LocalStorageTests.prototype);
})();
(function () {
    window.TestCase('PackedStringTests', Text.PackedStringTests.prototype);
})();
(function () {
    window.TestCase('CSyntaxTests', Interop.CSyntaxTests.prototype);
})();

}
ss.loader.registerScript('ScriptApp.System.Tests', ['Script.WebEx'], executeScript);
})();

//! This script was generated using Script# v0.6.3.0
