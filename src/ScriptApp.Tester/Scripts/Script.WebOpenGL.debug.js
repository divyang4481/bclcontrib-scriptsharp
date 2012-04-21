//! Script.WebOpenGL.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('SystemEx.Interop.OpenGL');

////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.OpenGL._webGLBufferData

SystemEx.Interop.OpenGL.$create__webGLBufferData = function SystemEx_Interop_OpenGL__webGLBufferData() { return {}; }


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.OpenGL.WebGL15ContextImpl

SystemEx.Interop.OpenGL.WebGL15ContextImpl = function SystemEx_Interop_OpenGL_WebGL15ContextImpl(canvas) {
    /// <param name="canvas" type="Object" domElement="true">
    /// </param>
    /// <field name="_logCount$1" type="Number" integer="true">
    /// </field>
    /// <field name="_smalL_BUF_COUNT$1" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_uMvpMatrix$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uSampler0$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uSampler1$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uTexEnv0$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uTexEnv1$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uEnableTexture0$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uEnableTexture1$1" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_buffers$1" type="Array" elementType="_webGLBufferData">
    /// </field>
    /// <field name="gl" type="WebGLRenderingContext">
    /// </field>
    /// <field name="_clientActiveTexture$1" type="Number" integer="true">
    /// </field>
    /// <field name="_activeTexture$1" type="Number" integer="true">
    /// </field>
    /// <field name="_boundTextureId$1" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_texEnvMode$1" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_elementBuffer$1" type="WebGLBuffer">
    /// </field>
    /// <field name="_staticDrawBuffers$1" type="Array" elementType="WebGLBuffer">
    /// </field>
    /// <field name="_textures$1" type="Array" elementType="WebGLTexture">
    /// </field>
    /// <field name="_textureFormats$1" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_textureFormat$1" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_canvas$1" type="Object" domElement="true">
    /// </field>
    this._buffers$1 = new Array(SystemEx.Interop.OpenGL.WebGL15ContextImpl._smalL_BUF_COUNT$1);
    this._boundTextureId$1 = new Array(2);
    this._texEnvMode$1 = new Array(2);
    this._staticDrawBuffers$1 = new Array(0);
    this._textures$1 = new Array(0);
    this._textureFormats$1 = new Array(0);
    this._textureFormat$1 = new Array(0);
    SystemEx.Interop.OpenGL.WebGL15ContextImpl.initializeBase(this, [ canvas.width, canvas.height ]);
    this._canvas$1 = canvas;
    this.gl = canvas.getContextWebGL();
    if (this.gl == null) {
        throw new Error('UnsupportedOperationException: WebGL N/A');
    }
    this._initShaders$1();
    this._checkError$1('initShader');
    this._elementBuffer$1 = this.gl.createBuffer();
    this._checkError$1('createBuffer f. elements');
    for (var index = 0; index < this._buffers$1.length; index++) {
        var b = SystemEx.Interop.OpenGL.$create__webGLBufferData();
        b.toBind = null;
        b.buffer = this.gl.createBuffer();
        this._checkError$1('createBuffer' + index);
        b.stride = 0;
        b.size = 0;
        b.type = 0;
        b.byteSize = 0;
        b.normalized = false;
        this._buffers$1[index] = b;
    }
}
SystemEx.Interop.OpenGL.WebGL15ContextImpl.prototype = {
    _logCount$1: 0,
    
    _log$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_log$1(msg) {
        /// <param name="msg" type="String">
        /// </param>
        if (this._logCount$1 >= 1000) {
            return;
        }
        console.log(this._logCount$1++ + ': ' + msg);
    },
    
    _checkError$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_checkError$1(s) {
        /// <param name="s" type="String">
        /// </param>
        var err = this.gl.getError();
        if (err !== GLES20.nO_ERROR) {
            this._log$1('GL_ERROR in ' + s + '(): ' + err);
        }
    },
    
    _uMvpMatrix$1: null,
    _uSampler0$1: null,
    _uSampler1$1: null,
    _uTexEnv0$1: null,
    _uTexEnv1$1: null,
    _uEnableTexture0$1: null,
    _uEnableTexture1$1: null,
    gl: null,
    _clientActiveTexture$1: 0,
    _activeTexture$1: 0,
    _elementBuffer$1: null,
    _canvas$1: null,
    
    _initShaders$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_initShaders$1() {
        var vertexShader = this._loadShader$1(GLES20.verteX_SHADER, '\r\n        attribute vec4 a_position;\r\n        attribute vec4 a_color;\r\n        attribute vec2 a_texCoord0;\r\n        attribute vec2 a_texCoord1;\r\n        uniform mat4 u_mvpMatrix;\r\n        varying vec4 v_color;\r\n        varying vec2 v_texCoord0;\r\n        varying vec2 v_texCoord1;\r\n        void main()\r\n        {\r\n            gl_Position = u_mvpMatrix * a_position;\r\n            v_color = a_color;\r\n            v_texCoord0 = a_texCoord0;\r\n            v_texCoord1 = a_texCoord1;\r\n        }');
        var fragmentShader = this._loadShader$1(GLES20.fragmenT_SHADER, '\r\n        #ifdef GL_ES\r\n        precision mediump float;\r\n        #endif\r\n        uniform sampler2D s_texture0;\r\n        uniform sampler2D s_texture1;\r\n        uniform int s_texEnv0;\r\n        uniform int s_texEnv1;\r\n        uniform int u_enable_texture_0;\r\n        uniform int u_enable_texture_1;\r\n        varying vec4 v_color;\r\n        varying vec2 v_texCoord0;\r\n        varying vec2 v_texCoord1;\r\n        vec4 finalColor;\r\n        void main()\r\n        {\r\n            finalColor = v_color;\r\n            if (u_enable_texture_0 == 1)\r\n            {\r\n                vec4 texel = texture2D(s_texture0, v_texCoord0);\r\n                if (s_texEnv0 == 1) {\r\n                    finalColor = finalColor * texel;\r\n                } else if (s_texEnv0 == 2) {\r\n                    finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);\r\n                } else {\r\n                    finalColor = texel;\r\n                }\r\n            }\r\n            if (u_enable_texture_1 == 1) {\r\n                vec4 texel = texture2D(s_texture1, v_texCoord1);\r\n                if (s_texEnv1 == 1) {\r\n                    finalColor = finalColor * texel;\r\n                } else if (s_texEnv1 == 2) {\r\n                    finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);\r\n                } else {\r\n                    finalColor = texel;\r\n                }\r\n            }\r\n            // simple alpha check\r\n            if (finalColor.a == 0.0) {\r\n                discard;\r\n            }\r\n            float gamma = 1.5;\r\n            float igamma = 1.0 / gamma;\r\n            gl_FragColor = vec4(pow(finalColor.r, igamma), pow(finalColor.g, igamma), pow(finalColor.b, igamma), finalColor.a);\r\n        }');
        if ((vertexShader == null) || (fragmentShader == null)) {
            throw new Error('RuntimeException: shader error');
        }
        var programObject = this.gl.createProgram();
        if ((programObject == null) || (this.gl.getError() !== GLES20.nO_ERROR)) {
            throw new Error('RuntimeException: program error');
        }
        this.gl.attachShader(programObject, vertexShader);
        this.gl.attachShader(programObject, fragmentShader);
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGL15Context.arraY_POSITION, 'a_position');
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR, 'a_color');
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0, 'a_texCoord0');
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_1, 'a_texCoord1');
        this.gl.linkProgram(programObject);
        this._uMvpMatrix$1 = this.gl.getUniformLocation(programObject, 'u_mvpMatrix');
        this._uSampler0$1 = this.gl.getUniformLocation(programObject, 's_texture0');
        this._uSampler1$1 = this.gl.getUniformLocation(programObject, 's_texture1');
        this._uTexEnv0$1 = this.gl.getUniformLocation(programObject, 's_texEnv0');
        this._uTexEnv1$1 = this.gl.getUniformLocation(programObject, 's_texEnv1');
        this._uEnableTexture0$1 = this.gl.getUniformLocation(programObject, 'u_enable_texture_0');
        this._uEnableTexture1$1 = this.gl.getUniformLocation(programObject, 'u_enable_texture_1');
        var linked = this.gl.getProgramParameter(programObject, GLES20.linK_STATUS);
        if (!linked) {
            throw new Error('RuntimeException: linker Error: ' + this.gl.getProgramInfoLog(programObject));
        }
        this.gl.useProgram(programObject);
        this.gl.uniform1i(this._uSampler0$1, 0);
        this.gl.uniform1i(this._uSampler1$1, 1);
        this.gl.activeTexture(GLES20.texturE0);
    },
    
    _loadShader$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_loadShader$1(shaderType, shaderSource) {
        /// <param name="shaderType" type="Number" integer="true">
        /// </param>
        /// <param name="shaderSource" type="String">
        /// </param>
        /// <returns type="WebGLShader"></returns>
        var shader = this.gl.createShader(shaderType);
        if (shader == null) {
            throw new Error('RuntimeException:');
        }
        this.gl.shaderSource(shader, shaderSource);
        this.gl.compileShader(shader);
        var compiled = this.gl.getShaderParameter(shader, GLES20.compilE_STATUS);
        if (!compiled) {
            throw new Error('RuntimeException: Shader compile error: ' + this.gl.getShaderInfoLog(shader));
        }
        return shader;
    },
    
    _getTextureMode$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_getTextureMode$1(indx) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return ((this._texEnvMode$1[indx] === GLES20.REPLACE) ? 0 : ((this._textureFormats$1[this._boundTextureId$1[indx]] === 3) ? 2 : 1));
    },
    
    _updatTCBuffer$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_updatTCBuffer$1(s, offset, count) {
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="count" type="Number" integer="true">
        /// </param>
        var b = this._buffers$1[SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0];
        this.gl.bindBuffer(GLES20.arraY_BUFFER, b.buffer);
        var p = s.get_position();
        var l = s.get_length();
        s.set_position(p + offset);
        s.setLength(p + offset + count);
        this.gl.bufferSubData(GLES20.arraY_BUFFER, offset * 4, this._getTypedArray$1(s, GLES20.FLOAT));
        s.set_position(p);
        s.setLength(l);
    },
    
    _prepareDraw$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_prepareDraw$1() {
        if (this.updateMvpMatrix()) {
            this.gl.uniformMatrix4fv(this._uMvpMatrix$1, false, this._mvpMatrix);
            this._checkError$1('prepareDraw');
        }
        this.gl.uniform1i(this._uTexEnv0$1, this._getTextureMode$1(0));
        this.gl.uniform1i(this._uTexEnv1$1, this._getTextureMode$1(1));
        for (var indx = 0; indx < SystemEx.Interop.OpenGL.WebGL15ContextImpl._smalL_BUF_COUNT$1; indx++) {
            var b = this._buffers$1[indx];
            if (b.toBind != null) {
                this.gl.bindBuffer(GLES20.arraY_BUFFER, b.buffer);
                this._checkError$1('bindBuffer' + indx);
                this.gl.bufferData(GLES20.arraY_BUFFER, b.toBind, GLES20.streaM_DRAW);
                this._checkError$1('bufferData' + indx);
                this.gl.vertexAttribPointer(indx, b.size, b.type, b.normalized, b.stride, 0);
                this._checkError$1('vertexAttribPointer');
                b.toBind = null;
            }
        }
    },
    
    _vertexAttribPointer$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_vertexAttribPointer$1(indx, size, type, normalize, stride, pointer) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="normalize" type="Boolean">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        var b = this._buffers$1[indx];
        b.stride = stride;
        b.size = size;
        b.normalized = normalize;
        b.type = type;
        b.toBind = this._getTypedArray$1(pointer, type);
    },
    
    _vertexAttribStaticDrawPointer$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_vertexAttribStaticDrawPointer$1(indx, size, type, normalized, stride, offset, pointer, staticDrawIndex) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="normalized" type="Boolean">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="staticDrawIndex" type="Number" integer="true">
        /// </param>
        var b = this._staticDrawBuffers$1[staticDrawIndex];
        if (b == null) {
            this._staticDrawBuffers$1[staticDrawIndex] = b = this.gl.createBuffer();
            this.gl.bindBuffer(GLES20.arraY_BUFFER, b);
            this.gl.bufferData(GLES20.arraY_BUFFER, this._getTypedArray$1(pointer, type), GLES20.statiC_DRAW);
            this._checkError$1('bufferData');
        }
        this.gl.bindBuffer(GLES20.arraY_BUFFER, b);
        this.gl.vertexAttribPointer(indx, size, type, normalized, stride, offset);
        this._checkError$1('vertexAttribPointer');
        this._buffers$1[indx].toBind = null;
    },
    
    _getTypedArray$1: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$_getTypedArray$1(s, type) {
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <returns type="ArrayBufferView"></returns>
        var memoryStream = (Type.safeCast(s, SystemEx.IO.MemoryStream));
        var array = null;
        var remainingBytes = (s.get_length() - s.get_position());
        var byteOffset = 0;
        switch (type) {
            case GLES20.FLOAT:
                return new Float32Array(array.buffer, byteOffset, remainingBytes / 4);
            case GLES20.unsigneD_BYTE:
                return new Uint8Array(array.buffer, byteOffset, remainingBytes);
            case GLES20.unsigneD_SHORT:
                return new Uint16Array(array.buffer, byteOffset, remainingBytes / 2);
            case GLES20.INT:
                return new Int32Array(array.buffer, byteOffset, remainingBytes / 4);
            case GLES20.SHORT:
                return new Int16Array(array.buffer, byteOffset, remainingBytes / 2);
            case GLES20.BYTE:
                return new Int8Array(array.buffer, byteOffset, remainingBytes);
        }
        throw new Error('IllegalArgumentException:');
    },
    
    clientActiveTexture: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$clientActiveTexture(texture) {
        /// <param name="texture" type="Number" integer="true">
        /// </param>
        this._clientActiveTexture$1 = texture - GLES20.texturE0;
    },
    
    cullFace: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$cullFace(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        this.gl.cullFace(mode);
        this._checkError$1('glCullFace');
    },
    
    pointSize: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$pointSize(size) {
        /// <param name="size" type="Number">
        /// </param>
    },
    
    polygonMode: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$polygonMode(face, mode) {
        /// <param name="face" type="Number" integer="true">
        /// </param>
        /// <param name="mode" type="Number" integer="true">
        /// </param>
    },
    
    scissor: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$scissor(x, y, width, height) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        this.gl.scissor(x, y, width, height);
        this._checkError$1('glScissor');
    },
    
    texParameterf: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texParameterf(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number">
        /// </param>
        this.gl.texParameterf(target, pname, param);
        this._checkError$1('glTexParameterf');
    },
    
    texParameterfv: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texParameterfv(target, pname, s) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    texParameteri: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texParameteri(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this.gl.texParameteri(target, pname, param);
        this._checkError$1('glTexParameteri');
    },
    
    texParameteriv: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texParameteriv(target, pname, s) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    texImage2D: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texImage2D(target, level, internalformat, width, height, border, format, type, pixels) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="border" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="pixels" type="SystemEx.IO.Stream">
        /// </param>
        this._textureFormat$1[this._boundTextureId$1[this._activeTexture$1]] = internalformat;
        this.gl.texImage2D(target, level, internalformat, width, height, border, format, type, this._getTypedArray$1(pixels, type));
        this._checkError$1('glTexImage2D');
    },
    
    texImage2Di: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texImage2Di(target, level, internalformat, format, type, image) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="image" type="Object" domElement="true">
        /// </param>
        this.gl.texImage2D(target, level, internalformat, format, type, image);
        this._checkError$1('texImage2D');
    },
    
    texImage2De: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texImage2De(target, level, internalformat, format, type, canvas) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="canvas" type="Object" domElement="true">
        /// </param>
        this.gl.texImage2D(target, level, internalformat, format, type, canvas);
        this._checkError$1('texImage2D');
    },
    
    texImage2Dex: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texImage2Dex(target, level, internalformat, format, type, canvas) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="canvas" type="Object" domElement="true">
        /// </param>
        this.gl.texImage2D(target, level, internalformat, format, type, canvas);
        this._checkError$1('texImage2D');
    },
    
    drawBuffer: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$drawBuffer(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
    },
    
    clear: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$clear(mask) {
        /// <param name="mask" type="Number" integer="true">
        /// </param>
        this.gl.clear(mask);
        this._checkError$1('glClear');
    },
    
    clearColor: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$clearColor(red, green, blue, alpha) {
        /// <param name="red" type="Number">
        /// </param>
        /// <param name="green" type="Number">
        /// </param>
        /// <param name="blue" type="Number">
        /// </param>
        /// <param name="alpha" type="Number">
        /// </param>
        this.gl.clearColor(red, green, blue, alpha);
        this._checkError$1('glClearColor');
    },
    
    depthMask: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$depthMask(flag) {
        /// <param name="flag" type="Boolean">
        /// </param>
        this.gl.depthMask(flag);
        this._checkError$1('glDepthMask');
    },
    
    disable: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$disable(cap) {
        /// <param name="cap" type="Number" integer="true">
        /// </param>
        if (cap !== GL15.texturE_2D) {
            this.gl.disable(cap);
            this._checkError$1('glDisable');
            return;
        }
        switch (this._activeTexture$1) {
            case 0:
                this.gl.uniform1i(this._uEnableTexture0$1, 0);
                break;
            case 1:
                this.gl.uniform1i(this._uEnableTexture1$1, 0);
                break;
            default:
                throw new Error('RuntimeException:');
        }
    },
    
    enable: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$enable(cap) {
        /// <param name="cap" type="Number" integer="true">
        /// </param>
        if (cap !== GL15.texturE_2D) {
            this.gl.enable(cap);
            this._checkError$1('glEnable');
            return;
        }
        switch (this._activeTexture$1) {
            case 0:
                this.gl.uniform1i(this._uEnableTexture0$1, 1);
                break;
            case 1:
                this.gl.uniform1i(this._uEnableTexture1$1, 1);
                break;
            default:
                throw new Error('RuntimeException:');
        }
    },
    
    finish: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$finish() {
        this.gl.finish();
    },
    
    blendFunc: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$blendFunc(sfactor, dfactor) {
        /// <param name="sfactor" type="Number" integer="true">
        /// </param>
        /// <param name="dfactor" type="Number" integer="true">
        /// </param>
        this.gl.blendFunc(sfactor, dfactor);
        this._checkError$1('glBlendFunc');
    },
    
    depthFunc: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$depthFunc(func) {
        /// <param name="func" type="Number" integer="true">
        /// </param>
        this.gl.depthFunc(func);
        this._checkError$1('glDepthFunc');
    },
    
    pixelStorei: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$pixelStorei(pname, param) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this.gl.pixelStorei(pname, param);
    },
    
    readPixels: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$readPixels(x, y, width, height, format, type, pixels) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="pixels" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    getError: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$getError() {
        /// <returns type="Number" integer="true"></returns>
        return this.gl.getError();
    },
    
    getString: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$getString(name) {
        /// <param name="name" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return 'glGetString not implemented';
    },
    
    depthRange: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$depthRange(zNear, zFar) {
        /// <param name="zNear" type="Number">
        /// </param>
        /// <param name="zFar" type="Number">
        /// </param>
        this.gl.depthRange(zNear, zFar);
        this._checkError$1('glDepthRangef');
    },
    
    viewport: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$viewport(x, y, width, height) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        SystemEx.Interop.OpenGL.WebGL15ContextImpl.callBaseMethod(this, 'viewport', [ x, y, width, height ]);
        this.gl.viewport(x, y, width, height);
        this._checkError$1('glViewport');
    },
    
    shadeModel: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$shadeModel(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
    },
    
    texEnvi: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texEnvi(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this._texEnvMode$1[this._activeTexture$1] = param;
    },
    
    texEnviv: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texEnviv(target, pname, pparams) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    drawArrays: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$drawArrays(mode, first, count) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        /// <param name="first" type="Number" integer="true">
        /// </param>
        /// <param name="count" type="Number" integer="true">
        /// </param>
        this._prepareDraw$1();
        this.gl.drawArrays(mode, first, count);
        this._checkError$1('drawArrays');
    },
    
    drawElements: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$drawElements(mode, count, type, indicies) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        /// <param name="count" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="indicies" type="SystemEx.IO.Stream">
        /// </param>
        this._prepareDraw$1();
        this.gl.bindBuffer(GL15.elemenT_ARRAY_BUFFER, this._elementBuffer$1);
        this._checkError$1('bindBuffer(el)');
        this.gl.bufferData(GL15.elemenT_ARRAY_BUFFER, this._getTypedArray$1(indicies, GL15.unsigneD_SHORT), GL15.dynamiC_DRAW);
        this._checkError$1('bufferData(el)');
        this.gl.drawElements(mode, count, GL15.unsigneD_SHORT, 0);
        this._checkError$1('drawElements');
    },
    
    texSubImage2D: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="xoffset" type="Number" integer="true">
        /// </param>
        /// <param name="yoffset" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="pixels" type="SystemEx.IO.Stream">
        /// </param>
        this.gl.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, this._getTypedArray$1(pixels, type));
        this._checkError$1('glTexSubImage2D');
    },
    
    bindTexture: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$bindTexture(target, texture) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="texture" type="Number" integer="true">
        /// </param>
        var t = this._textures$1[texture];
        if (t == null) {
            t = this.gl.createTexture();
            this._textures$1[texture] = t;
        }
        this.gl.bindTexture(target, t);
        this._checkError$1('glBindTexture');
        this._boundTextureId$1[this._activeTexture$1] = texture;
    },
    
    deleteTextures: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$deleteTextures(n, textures) {
        /// <param name="n" type="Number" integer="true">
        /// </param>
        /// <param name="textures" type="SystemEx.IO.Stream">
        /// </param>
        for (var index = 0; index < n; index++) {
            var textureIndex = SystemEx.IO.SE.readInt32(textures);
            this.gl.deleteTexture(this._textures$1[textureIndex]);
            this._checkError$1('glDeleteTexture');
            this._textures$1[textureIndex] = null;
        }
    },
    
    color4f: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$color4f(red, green, blue, alpha) {
        /// <param name="red" type="Number">
        /// </param>
        /// <param name="green" type="Number">
        /// </param>
        /// <param name="blue" type="Number">
        /// </param>
        /// <param name="alpha" type="Number">
        /// </param>
        this.gl.vertexAttrib4f(SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR, red, green, blue, alpha);
        this._checkError$1('glColor4f');
    },
    
    colorPointer: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$colorPointer(size, type, stride, pointer) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        this._vertexAttribPointer$1(SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR, size, type, true, stride, pointer);
        this._checkError$1('glColorPointer');
    },
    
    disableClientState: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$disableClientState(array) {
        /// <param name="array" type="Number" integer="true">
        /// </param>
        switch (array) {
            case GLES11.coloR_ARRAY:
                this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR);
                this._checkError$1('DisableClientState colorArr');
                break;
            case GLES11.verteX_ARRAY:
                this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_POSITION);
                this._checkError$1('DisableClientState vertexArrr');
                break;
            case GLES11.texturE_COORD_ARRAY:
                switch (this._clientActiveTexture$1) {
                    case 0:
                        this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0);
                        this._checkError$1('DisableClientState texCoord0');
                        break;
                    case 1:
                        this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_1);
                        this._checkError$1('DisableClientState texCoord1');
                        break;
                    default:
                        throw new Error('RuntimeException:');
                }
                break;
            default:
                this._log$1('unsupported / unrecogized client state');
                break;
        }
    },
    
    enableClientState: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$enableClientState(array) {
        /// <param name="array" type="Number" integer="true">
        /// </param>
        switch (array) {
            case GLES11.coloR_ARRAY:
                this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR);
                this._checkError$1('enableClientState colorArr');
                break;
            case GLES11.verteX_ARRAY:
                this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_POSITION);
                this._checkError$1('enableClientState vertexArrr');
                break;
            case GLES11.texturE_COORD_ARRAY:
                switch (this._clientActiveTexture$1) {
                    case 0:
                        this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0);
                        this._checkError$1('enableClientState texCoord0');
                        break;
                    case 1:
                        this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_1);
                        this._checkError$1('enableClientState texCoord1');
                        break;
                    default:
                        throw new Error('RuntimeException:');
                }
                break;
            default:
                this._log$1('unsupported / unrecogized client state ' + array);
                break;
        }
    },
    
    texCoordPointer: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$texCoordPointer(size, type, stride, pointer) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        this._vertexAttribPointer$1(SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0 + this._clientActiveTexture$1, size, type, false, stride, pointer);
        this._checkError$1('texCoordPointer');
    },
    
    vertexPointer: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$vertexPointer(size, type, stride, pointer) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        this._vertexAttribPointer$1(SystemEx.Interop.OpenGL.WebGL15Context.arraY_POSITION, size, type, false, stride, pointer);
        this._checkError$1('glVertexPointer');
    },
    
    activeTexture: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$activeTexture(texture) {
        /// <param name="texture" type="Number" integer="true">
        /// </param>
        this.gl.activeTexture(texture);
        this._checkError$1('glActiveTexture');
        this._activeTexture$1 = texture - GL15.texturE0;
    },
    
    pointParameterf: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$pointParameterf(pname, param) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number">
        /// </param>
    },
    
    pointParameterfv: function SystemEx_Interop_OpenGL_WebGL15ContextImpl$pointParameterfv(pname, s) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.OpenGL.WebGL15Context

SystemEx.Interop.OpenGL.WebGL15Context = function SystemEx_Interop_OpenGL_WebGL15Context(width, height) {
    /// <param name="width" type="Number" integer="true">
    /// </param>
    /// <param name="height" type="Number" integer="true">
    /// </param>
    /// <field name="_begiN_END_MAX_VERTICES" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_POSITION" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_COLOR" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_TEXCOORD_0" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_TEXCOORD_1" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_mode" type="Number" integer="true">
    /// </field>
    /// <field name="_texCoordBuf" type="SystemEx.IO.Stream">
    /// </field>
    /// <field name="_vertexBuf" type="SystemEx.IO.Stream">
    /// </field>
    /// <field name="_st0011" type="SystemEx.IO.Stream">
    /// </field>
    /// <field name="_st0011BufferId" type="Number" integer="true">
    /// </field>
    /// <field name="_matrixMode" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportX" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportY" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportW" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportH" type="Number" integer="true">
    /// </field>
    /// <field name="_projectionMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_modelViewMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_textureMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_currentMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_projectionMatrixStack" type="Array">
    /// </field>
    /// <field name="_modelViewMatrixStack" type="Array">
    /// </field>
    /// <field name="_textureMatrixStack" type="Array">
    /// </field>
    /// <field name="_currentMatrixStack" type="Array">
    /// </field>
    /// <field name="_tmpMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_mvpMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_mvpDirty" type="Boolean">
    /// </field>
    /// <field name="_width" type="Number" integer="true">
    /// </field>
    /// <field name="_height" type="Number" integer="true">
    /// </field>
    this._texCoordBuf = new SystemEx.IO.MemoryStream(new Array(SystemEx.Interop.OpenGL.WebGL15Context._begiN_END_MAX_VERTICES * 2 * 4));
    this._vertexBuf = new SystemEx.IO.MemoryStream(new Array(SystemEx.Interop.OpenGL.WebGL15Context._begiN_END_MAX_VERTICES * 3 * 4));
    this._st0011 = new SystemEx.IO.MemoryStream(new Array(8 * 4));
    this._matrixMode = GLES11.MODELVIEW;
    this._projectionMatrix = new Array(16);
    this._modelViewMatrix = new Array(16);
    this._textureMatrix = new Array(16);
    this._projectionMatrixStack = [];
    this._modelViewMatrixStack = [];
    this._textureMatrixStack = [];
    this._tmpMatrix = new Array(16);
    this._mvpMatrix = new Array(16);
    SystemEx.IO.SE.writeSingle(this._st0011, 0);
    SystemEx.IO.SE.writeSingle(this._st0011, 0);
    SystemEx.IO.SE.writeSingle(this._st0011, 1);
    SystemEx.IO.SE.writeSingle(this._st0011, 0);
    SystemEx.IO.SE.writeSingle(this._st0011, 1);
    SystemEx.IO.SE.writeSingle(this._st0011, 1);
    SystemEx.IO.SE.writeSingle(this._st0011, 0);
    SystemEx.IO.SE.writeSingle(this._st0011, 1);
    this._st0011.set_position(0);
    SystemEx.MathMatrix.setIdentityM(this._modelViewMatrix, 0);
    SystemEx.MathMatrix.setIdentityM(this._projectionMatrix, 0);
    SystemEx.MathMatrix.setIdentityM(this._textureMatrix, 0);
    this._width = width;
    this._height = height;
}
SystemEx.Interop.OpenGL.WebGL15Context.prototype = {
    _mode: 0,
    _st0011BufferId: 0,
    _viewportX: 0,
    _viewportY: 0,
    _viewportW: 0,
    _viewportH: 0,
    _currentMatrix: null,
    _currentMatrixStack: null,
    _mvpDirty: true,
    _width: 0,
    _height: 0,
    
    updateMvpMatrix: function SystemEx_Interop_OpenGL_WebGL15Context$updateMvpMatrix() {
        /// <returns type="Boolean"></returns>
        if (!this._mvpDirty) {
            return false;
        }
        SystemEx.MathMatrix.multiplyMM(this._mvpMatrix, 0, this._projectionMatrix, 0, this._modelViewMatrix, 0);
        this._mvpDirty = false;
        return true;
    },
    
    _project: function SystemEx_Interop_OpenGL_WebGL15Context$_project(objX, objY, objZ, view, win) {
        /// <param name="objX" type="Number">
        /// </param>
        /// <param name="objY" type="Number">
        /// </param>
        /// <param name="objZ" type="Number">
        /// </param>
        /// <param name="view" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="win" type="Array" elementType="Number">
        /// </param>
        /// <returns type="Boolean"></returns>
        var v = [ objX, objY, objZ, 1 ];
        var v2 = new Array(4);
        SystemEx.MathMatrix.multiplyMV(v2, 0, this._mvpMatrix, 0, v, 0);
        var w = v2[3];
        if (w === 0) {
            return false;
        }
        var rw = 1 / w;
        win[0] = this._viewportX + this._viewportW * (v2[0] * rw + 1) * 0.5;
        win[1] = this._viewportY + this._viewportH * (v2[1] * rw + 1) * 0.5;
        win[2] = (v2[2] * rw + 1) * 0.5;
        return true;
    },
    
    _vertexAttribStaticDrawPointer2: function SystemEx_Interop_OpenGL_WebGL15Context$_vertexAttribStaticDrawPointer2(indx, size, type, normalized, stride, offset, pointer, staticDrawIndex) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="normalized" type="Boolean">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="staticDrawIndex" type="Number" integer="true">
        /// </param>
        var b = pointer;
        if ((type === GL15.BYTE) || (type === GL15.unsigneD_BYTE)) {
            var p = b.get_position();
            b.set_position(p + offset);
            if (indx !== SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR) {
                throw new Error('InvalidOperationException:');
            }
            this.colorPointer(size, type, stride, b);
            b.set_position(p);
        }
        else if (type === GL15.FLOAT) {
            var p = b.get_position();
            b.set_position(p + offset / 4);
            switch (indx) {
                case SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR:
                    this.colorPointer(size, type, stride, b);
                    break;
                case SystemEx.Interop.OpenGL.WebGL15Context.arraY_POSITION:
                    this.vertexPointer(size, type, stride, b);
                    break;
                case SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0:
                case SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_1:
                    this.texCoordPointer(size, type, stride, b);
                    break;
                default:
                    throw new Error('ArgumentException: nioBuffer');
            }
            b.set_position(p);
        }
    },
    
    getFloatv: function SystemEx_Interop_OpenGL_WebGL15Context$getFloatv(pname, s) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        switch (pname) {
            case GLES11.MODELVIEW:
            case GLES11.modelvieW_MATRIX:
                var p = s.get_position();
                for (var index = 0; index < this._modelViewMatrix.length; index++) {
                    SystemEx.IO.SE.writeSingle(s, this._modelViewMatrix[index]);
                }
                s.set_position(p);
                break;
            default:
                throw new Error('ArgumentException: glGetFloat');
        }
    },
    
    getIntegerv: function SystemEx_Interop_OpenGL_WebGL15Context$getIntegerv(pname, s) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        switch (pname) {
            case GLES11.matriX_MODE:
                SystemEx.IO.SE.writeUInt32(s, this._matrixMode);
                break;
            default:
                throw new Error('ArgumentException:');
        }
    },
    
    viewport: function SystemEx_Interop_OpenGL_WebGL15Context$viewport(x, y, width, height) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        this._viewportX = x;
        this._viewportY = y;
        this._viewportW = width;
        this._viewportH = height;
    },
    
    begin: function SystemEx_Interop_OpenGL_WebGL15Context$begin(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        this._mode = mode;
        this._vertexBuf.setLength(0);
        this._texCoordBuf.setLength(0);
    },
    
    color3f: function SystemEx_Interop_OpenGL_WebGL15Context$color3f(red, green, blue) {
        /// <param name="red" type="Number">
        /// </param>
        /// <param name="green" type="Number">
        /// </param>
        /// <param name="blue" type="Number">
        /// </param>
        this.color4f(red, green, blue, 1);
    },
    
    color3ub: function SystemEx_Interop_OpenGL_WebGL15Context$color3ub(red, green, blue) {
        /// <param name="red" type="Number" integer="true">
        /// </param>
        /// <param name="green" type="Number" integer="true">
        /// </param>
        /// <param name="blue" type="Number" integer="true">
        /// </param>
        this.color4ub(red, green, blue, 255);
    },
    
    color4ub: function SystemEx_Interop_OpenGL_WebGL15Context$color4ub(red, green, blue, alpha) {
        /// <param name="red" type="Number" integer="true">
        /// </param>
        /// <param name="green" type="Number" integer="true">
        /// </param>
        /// <param name="blue" type="Number" integer="true">
        /// </param>
        /// <param name="alpha" type="Number" integer="true">
        /// </param>
        this.color4f((red & 255) / 255, (green & 255) / 255, (blue & 255) / 255, (alpha & 255) / 255);
    },
    
    end: function SystemEx_Interop_OpenGL_WebGL15Context$end() {
        var count = this._vertexBuf.get_position() / 3;
        this._vertexBuf.set_position(0);
        this.enableClientState(GLES11.verteX_ARRAY);
        this.vertexPointer(3, 0, 0, this._vertexBuf);
        if (this._mode === GLXX.simplE_TEXUTRED_QUAD) {
            this.enableClientState(GLES11.texturE_COORD_ARRAY);
            this._vertexAttribStaticDrawPointer2(SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0, 2, GL15.FLOAT, false, 0, 0, this._st0011, this._st0011BufferId);
            this.texCoordPointer(2, GL15.FLOAT, 0, this._st0011);
            this.drawArrays(GL15.trianglE_FAN, 0, 4);
        }
        else {
            if (this._texCoordBuf.get_position() > 0) {
                this._texCoordBuf.set_position(0);
                this.enableClientState(GLES11.texturE_COORD_ARRAY);
                this.texCoordPointer(2, GL15.FLOAT, 0, this._texCoordBuf);
            }
            if (this._mode === GLXX.QUADS) {
                for (var index = 0; index < count; index += 4) {
                    this.drawArrays(GL15.trianglE_FAN, index, 4);
                }
            }
            else {
                this.drawArrays(this._mode, 0, count);
            }
        }
    },
    
    texCoord2f: function SystemEx_Interop_OpenGL_WebGL15Context$texCoord2f(s, t) {
        /// <param name="s" type="Number">
        /// </param>
        /// <param name="t" type="Number">
        /// </param>
        SystemEx.IO.SE.writeSingle(this._texCoordBuf, s);
        SystemEx.IO.SE.writeSingle(this._texCoordBuf, t);
    },
    
    vertex2f: function SystemEx_Interop_OpenGL_WebGL15Context$vertex2f(x, y) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        this.vertex3f(x, y, 0);
    },
    
    vertex3f: function SystemEx_Interop_OpenGL_WebGL15Context$vertex3f(x, y, z) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        SystemEx.IO.SE.writeSingle(this._vertexBuf, x);
        SystemEx.IO.SE.writeSingle(this._vertexBuf, y);
        SystemEx.IO.SE.writeSingle(this._vertexBuf, z);
    },
    
    alphaFunc: function SystemEx_Interop_OpenGL_WebGL15Context$alphaFunc(func, rref) {
        /// <param name="func" type="Number" integer="true">
        /// </param>
        /// <param name="rref" type="Number">
        /// </param>
    },
    
    frustum: function SystemEx_Interop_OpenGL_WebGL15Context$frustum(left, right, bottom, top, zNear, zFar) {
        /// <param name="left" type="Number">
        /// </param>
        /// <param name="right" type="Number">
        /// </param>
        /// <param name="bottom" type="Number">
        /// </param>
        /// <param name="top" type="Number">
        /// </param>
        /// <param name="zNear" type="Number">
        /// </param>
        /// <param name="zFar" type="Number">
        /// </param>
        var a = 2 * zNear;
        var b = right - left;
        var c = top - bottom;
        var d = zFar - zNear;
        var matrix = [ (a / b), 0, 0, 0, 0, (a / c), 0, 0, ((right + left) / b), ((top + bottom) / c), ((-zFar - zNear) / d), -1, 0, 0, ((-a * zFar) / d), 0 ];
        this.multMatrixf(matrix);
    },
    
    loadIdentity: function SystemEx_Interop_OpenGL_WebGL15Context$loadIdentity() {
        SystemEx.MathMatrix.setIdentityM(this._currentMatrix, 0);
        this._mvpDirty = true;
    },
    
    loadMatrixf: function SystemEx_Interop_OpenGL_WebGL15Context$loadMatrixf(s) {
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        var p = s.get_position();
        for (var index = 0; index < this._currentMatrix.length; index++) {
            this._currentMatrix[index] = SystemEx.IO.SE.readSingle(s);
        }
        s.set_position(p);
        this._mvpDirty = true;
    },
    
    matrixMode: function SystemEx_Interop_OpenGL_WebGL15Context$matrixMode(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        switch (mode) {
            case GLES11.MODELVIEW:
                this._currentMatrix = this._modelViewMatrix;
                this._currentMatrixStack = this._modelViewMatrixStack;
                break;
            case GLES11.PROJECTION:
                this._currentMatrix = this._projectionMatrix;
                this._currentMatrixStack = this._projectionMatrixStack;
                break;
            case GL15.TEXTURE:
                this._currentMatrix = this._textureMatrix;
                this._currentMatrixStack = this._textureMatrixStack;
                break;
            default:
                throw new Error('ArgumentException: Unrecoginzed matrix mode: ' + mode);
        }
        this._matrixMode = mode;
    },
    
    multMatrixf: function SystemEx_Interop_OpenGL_WebGL15Context$multMatrixf(m) {
        /// <param name="m" type="Array" elementType="Number">
        /// </param>
        var ofs = 0;
        SystemEx.MathMatrix.multiplyMM(this._tmpMatrix, 0, this._currentMatrix, 0, m, ofs);
        SystemEx.JSArrayEx.copy(this._tmpMatrix, 0, this._currentMatrix, 0, 16);
        this._mvpDirty = true;
    },
    
    ortho: function SystemEx_Interop_OpenGL_WebGL15Context$ortho(left, right, bottom, top, zNear, zFar) {
        /// <param name="left" type="Number">
        /// </param>
        /// <param name="right" type="Number">
        /// </param>
        /// <param name="bottom" type="Number">
        /// </param>
        /// <param name="top" type="Number">
        /// </param>
        /// <param name="zNear" type="Number">
        /// </param>
        /// <param name="zFar" type="Number">
        /// </param>
        var matrix = [ 2 / (right - left), 0, 0, 0, 0, 2 / (top - bottom), 0, 0, 0, 0, -2 / (zFar - zNear), 0, -(right + left) / (right - left), -(top + bottom) / (top - bottom), -(zFar + zNear) / (zFar - zNear), 1 ];
        this.multMatrixf(matrix);
        this._mvpDirty = true;
    },
    
    orthox_: function SystemEx_Interop_OpenGL_WebGL15Context$orthox_(left, right, bottom, top, zNear, zFar) {
        /// <param name="left" type="Number" integer="true">
        /// </param>
        /// <param name="right" type="Number" integer="true">
        /// </param>
        /// <param name="bottom" type="Number" integer="true">
        /// </param>
        /// <param name="top" type="Number" integer="true">
        /// </param>
        /// <param name="zNear" type="Number" integer="true">
        /// </param>
        /// <param name="zFar" type="Number" integer="true">
        /// </param>
        var l = left;
        var r = right;
        var b = bottom;
        var n = zNear;
        var f = zFar;
        var t = top;
        var matrix = [ 2 / (r - l), 0, 0, 0, 0, 2 / (t - b), 0, 0, 0, 0, -2 / (f - n), 0, -(r + l) / (r - l), -(t + b) / (t - b), -(f + n) / (f - n), 1 ];
        this.multMatrixf(matrix);
        this._mvpDirty = true;
    },
    
    popMatrix: function SystemEx_Interop_OpenGL_WebGL15Context$popMatrix() {
        var top = this._currentMatrixStack.pop();
        SystemEx.JSArrayEx.copy(top, 0, this._currentMatrix, 0, 16);
        this._mvpDirty = true;
    },
    
    pushMatrix: function SystemEx_Interop_OpenGL_WebGL15Context$pushMatrix() {
        var copy = new Array(16);
        SystemEx.JSArrayEx.copy(this._currentMatrix, 0, copy, 0, 16);
        this._currentMatrixStack.push(copy);
    },
    
    rotatef: function SystemEx_Interop_OpenGL_WebGL15Context$rotatef(angle, x, y, z) {
        /// <param name="angle" type="Number">
        /// </param>
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        if ((x !== 0) || (y !== 0) || (z !== 0)) {
            SystemEx.MathMatrix.rotateM2(this._currentMatrix, 0, angle, x, y, z);
        }
        this._mvpDirty = true;
    },
    
    scalef: function SystemEx_Interop_OpenGL_WebGL15Context$scalef(x, y, z) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        SystemEx.MathMatrix.scaleM2(this._currentMatrix, 0, x, y, z);
        this._mvpDirty = true;
    },
    
    scalex_: function SystemEx_Interop_OpenGL_WebGL15Context$scalex_(x, y, z) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="z" type="Number" integer="true">
        /// </param>
        SystemEx.MathMatrix.scaleM2(this._currentMatrix, 0, x, y, z);
        this._mvpDirty = true;
    },
    
    translatef: function SystemEx_Interop_OpenGL_WebGL15Context$translatef(x, y, z) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        SystemEx.MathMatrix.translateM2(this._currentMatrix, 0, x, y, z);
        this._mvpDirty = true;
    },
    
    translatex_: function SystemEx_Interop_OpenGL_WebGL15Context$translatex_(x, y, z) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="z" type="Number" integer="true">
        /// </param>
        SystemEx.MathMatrix.translateM2(this._currentMatrix, 0, x, y, z);
        this._mvpDirty = true;
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl

SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl = function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl(canvas) {
    /// <param name="canvas" type="Object" domElement="true">
    /// </param>
    /// <field name="_logCount$3" type="Number" integer="true">
    /// </field>
    /// <field name="_smalL_BUF_COUNT$3" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_uMvpMatrix$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uSampler0$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uSampler1$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uTexEnv0$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uTexEnv1$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uEnableTexture0$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_uEnableTexture1$3" type="WebGLUniformLocation">
    /// </field>
    /// <field name="_buffers$3" type="Array" elementType="_webGLBufferData">
    /// </field>
    /// <field name="gl" type="WebGLRenderingContext">
    /// </field>
    /// <field name="_clientActiveTexture$3" type="Number" integer="true">
    /// </field>
    /// <field name="_activeTexture$3" type="Number" integer="true">
    /// </field>
    /// <field name="_boundTextureId$3" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_texEnvMode$3" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_elementBuffer$3" type="WebGLBuffer">
    /// </field>
    /// <field name="_staticDrawBuffers$3" type="Array" elementType="WebGLBuffer">
    /// </field>
    /// <field name="_textures$3" type="Array" elementType="WebGLTexture">
    /// </field>
    /// <field name="_textureFormats$3" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_textureFormat$3" type="Array" elementType="Number" elementInteger="true">
    /// </field>
    /// <field name="_canvas$3" type="Object" domElement="true">
    /// </field>
    this._buffers$3 = new Array(SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl._smalL_BUF_COUNT$3);
    this._boundTextureId$3 = new Array(2);
    this._texEnvMode$3 = new Array(2);
    this._staticDrawBuffers$3 = new Array(0);
    this._textures$3 = new Array(0);
    this._textureFormats$3 = new Array(0);
    this._textureFormat$3 = new Array(0);
    SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl.initializeBase(this, [ canvas.width, canvas.height ]);
    this._canvas$3 = canvas;
    this.gl = canvas.getContextWebGL();
    if (this.gl == null) {
        throw new Error('UnsupportedOperationException: WebGL N/A');
    }
    this._initShaders$3();
    this._checkError$3('initShader');
    this._elementBuffer$3 = this.gl.createBuffer();
    this._checkError$3('createBuffer f. elements');
    for (var index = 0; index < this._buffers$3.length; index++) {
        var b = SystemEx.Interop.OpenGL.$create__webGLBufferData();
        b.toBind = null;
        b.buffer = this.gl.createBuffer();
        this._checkError$3('createBuffer' + index);
        b.stride = 0;
        b.size = 0;
        b.type = 0;
        b.byteSize = 0;
        b.normalized = false;
        this._buffers$3[index] = b;
    }
}
SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl.prototype = {
    _logCount$3: 0,
    
    _log$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_log$3(msg) {
        /// <param name="msg" type="String">
        /// </param>
        if (this._logCount$3 >= 1000) {
            return;
        }
        console.log(this._logCount$3++ + ': ' + msg);
    },
    
    _checkError$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_checkError$3(s) {
        /// <param name="s" type="String">
        /// </param>
        var err = this.gl.getError();
        if (err !== GLES20.nO_ERROR) {
            this._log$3('GL_ERROR in ' + s + '(): ' + err);
        }
    },
    
    _uMvpMatrix$3: null,
    _uSampler0$3: null,
    _uSampler1$3: null,
    _uTexEnv0$3: null,
    _uTexEnv1$3: null,
    _uEnableTexture0$3: null,
    _uEnableTexture1$3: null,
    gl: null,
    _clientActiveTexture$3: 0,
    _activeTexture$3: 0,
    _elementBuffer$3: null,
    _canvas$3: null,
    
    _initShaders$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_initShaders$3() {
        var vertexShader = this._loadShader$3(GLES20.verteX_SHADER, '\r\n        attribute vec4 a_position;\r\n        attribute vec4 a_color;\r\n        attribute vec2 a_texCoord0;\r\n        attribute vec2 a_texCoord1;\r\n        uniform mat4 u_mvpMatrix;\r\n        varying vec4 v_color;\r\n        varying vec2 v_texCoord0;\r\n        varying vec2 v_texCoord1;\r\n        void main()\r\n        {\r\n            gl_Position = u_mvpMatrix * a_position;\r\n            v_color = a_color;\r\n            v_texCoord0 = a_texCoord0;\r\n            v_texCoord1 = a_texCoord1;\r\n        }');
        var fragmentShader = this._loadShader$3(GLES20.fragmenT_SHADER, '\r\n        #ifdef GL_ES\r\n        precision mediump float;\r\n        #endif\r\n        uniform sampler2D s_texture0;\r\n        uniform sampler2D s_texture1;\r\n        uniform int s_texEnv0;\r\n        uniform int s_texEnv1;\r\n        uniform int u_enable_texture_0;\r\n        uniform int u_enable_texture_1;\r\n        varying vec4 v_color;\r\n        varying vec2 v_texCoord0;\r\n        varying vec2 v_texCoord1;\r\n        vec4 finalColor;\r\n        void main()\r\n        {\r\n            finalColor = v_color;\r\n            if (u_enable_texture_0 == 1)\r\n            {\r\n                vec4 texel = texture2D(s_texture0, v_texCoord0);\r\n                if (s_texEnv0 == 1) {\r\n                    finalColor = finalColor * texel;\r\n                } else if (s_texEnv0 == 2) {\r\n                    finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);\r\n                } else {\r\n                    finalColor = texel;\r\n                }\r\n            }\r\n            if (u_enable_texture_1 == 1) {\r\n                vec4 texel = texture2D(s_texture1, v_texCoord1);\r\n                if (s_texEnv1 == 1) {\r\n                    finalColor = finalColor * texel;\r\n                } else if (s_texEnv1 == 2) {\r\n                    finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);\r\n                } else {\r\n                    finalColor = texel;\r\n                }\r\n            }\r\n            // simple alpha check\r\n            if (finalColor.a == 0.0) {\r\n                discard;\r\n            }\r\n            float gamma = 1.5;\r\n            float igamma = 1.0 / gamma;\r\n            gl_FragColor = vec4(pow(finalColor.r, igamma), pow(finalColor.g, igamma), pow(finalColor.b, igamma), finalColor.a);\r\n        }');
        if ((vertexShader == null) || (fragmentShader == null)) {
            throw new Error('RuntimeException: shader error');
        }
        var programObject = this.gl.createProgram();
        if ((programObject == null) || (this.gl.getError() !== GLES20.nO_ERROR)) {
            throw new Error('RuntimeException: program error');
        }
        this.gl.attachShader(programObject, vertexShader);
        this.gl.attachShader(programObject, fragmentShader);
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_POSITION, 'a_position');
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_COLOR, 'a_color');
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_0, 'a_texCoord0');
        this.gl.bindAttribLocation(programObject, SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_1, 'a_texCoord1');
        this.gl.linkProgram(programObject);
        this._uMvpMatrix$3 = this.gl.getUniformLocation(programObject, 'u_mvpMatrix');
        this._uSampler0$3 = this.gl.getUniformLocation(programObject, 's_texture0');
        this._uSampler1$3 = this.gl.getUniformLocation(programObject, 's_texture1');
        this._uTexEnv0$3 = this.gl.getUniformLocation(programObject, 's_texEnv0');
        this._uTexEnv1$3 = this.gl.getUniformLocation(programObject, 's_texEnv1');
        this._uEnableTexture0$3 = this.gl.getUniformLocation(programObject, 'u_enable_texture_0');
        this._uEnableTexture1$3 = this.gl.getUniformLocation(programObject, 'u_enable_texture_1');
        var linked = this.gl.getProgramParameter(programObject, GLES20.linK_STATUS);
        if (!linked) {
            throw new Error('RuntimeException: linker Error: ' + this.gl.getProgramInfoLog(programObject));
        }
        this.gl.useProgram(programObject);
        this.gl.uniform1i(this._uSampler0$3, 0);
        this.gl.uniform1i(this._uSampler1$3, 1);
        this.gl.activeTexture(GLES20.texturE0);
    },
    
    _loadShader$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_loadShader$3(shaderType, shaderSource) {
        /// <param name="shaderType" type="Number" integer="true">
        /// </param>
        /// <param name="shaderSource" type="String">
        /// </param>
        /// <returns type="WebGLShader"></returns>
        var shader = this.gl.createShader(shaderType);
        if (shader == null) {
            throw new Error('RuntimeException:');
        }
        this.gl.shaderSource(shader, shaderSource);
        this.gl.compileShader(shader);
        var compiled = this.gl.getShaderParameter(shader, GLES20.compilE_STATUS);
        if (!compiled) {
            throw new Error('RuntimeException: Shader compile error: ' + this.gl.getShaderInfoLog(shader));
        }
        return shader;
    },
    
    clearColor: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$clearColor(red, green, blue, alpha) {
        /// <param name="red" type="Number">
        /// </param>
        /// <param name="green" type="Number">
        /// </param>
        /// <param name="blue" type="Number">
        /// </param>
        /// <param name="alpha" type="Number">
        /// </param>
        this.gl.clearColor(red, green, blue, alpha);
        this._checkError$3('glClearColor');
    },
    
    color4f: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$color4f(red, green, blue, alpha) {
        /// <param name="red" type="Number">
        /// </param>
        /// <param name="green" type="Number">
        /// </param>
        /// <param name="blue" type="Number">
        /// </param>
        /// <param name="alpha" type="Number">
        /// </param>
        this.gl.vertexAttrib4f(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_COLOR, red, green, blue, alpha);
        this._checkError$3('glColor4f');
    },
    
    depthRangef: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$depthRangef(zNear, zFar) {
        /// <param name="zNear" type="Number">
        /// </param>
        /// <param name="zFar" type="Number">
        /// </param>
        this.gl.depthRange(zNear, zFar);
        this._checkError$3('glDepthRangef');
    },
    
    pointParameterf: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pointParameterf(pname, param) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number">
        /// </param>
    },
    
    pointParameterfv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pointParameterfv(pname, pparams) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    pointSize: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pointSize(size) {
        /// <param name="size" type="Number">
        /// </param>
    },
    
    texParameterf: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texParameterf(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number">
        /// </param>
        this.gl.texParameterf(target, pname, param);
        this._checkError$3('glTexParameterf');
    },
    
    texParameterfv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texParameterfv(target, pname, pparams) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    activeTexture: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$activeTexture(texture) {
        /// <param name="texture" type="Number" integer="true">
        /// </param>
        this.gl.activeTexture(texture);
        this._checkError$3('glActiveTexture');
        this._activeTexture$3 = texture - GLES20.texturE0;
    },
    
    bindTexture: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$bindTexture(target, texture) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="texture" type="Number" integer="true">
        /// </param>
        var t = this._textures$3[texture];
        if (t == null) {
            t = this.gl.createTexture();
            this._textures$3[texture] = t;
        }
        this.gl.bindTexture(target, t);
        this._checkError$3('glBindTexture');
        this._boundTextureId$3[this._activeTexture$3] = texture;
    },
    
    blendFunc: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$blendFunc(sfactor, dfactor) {
        /// <param name="sfactor" type="Number" integer="true">
        /// </param>
        /// <param name="dfactor" type="Number" integer="true">
        /// </param>
        this.gl.blendFunc(sfactor, dfactor);
        this._checkError$3('glBlendFunc');
    },
    
    clear: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$clear(mask) {
        /// <param name="mask" type="Number" integer="true">
        /// </param>
        this.gl.clear(mask);
        this._checkError$3('glClear');
    },
    
    clientActiveTexture: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$clientActiveTexture(texture) {
        /// <param name="texture" type="Number" integer="true">
        /// </param>
        this._clientActiveTexture$3 = texture - GLES20.texturE0;
    },
    
    colorPointer: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$colorPointer(size, type, stride, pointer) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        this._vertexAttribPointer$3(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_COLOR, size, type, true, stride, pointer);
        this._checkError$3('glColorPointer');
    },
    
    cullFace: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$cullFace(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        this.gl.cullFace(mode);
        this._checkError$3('glCullFace');
    },
    
    deleteTextures: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$deleteTextures(n, textures) {
        /// <param name="n" type="Number" integer="true">
        /// </param>
        /// <param name="textures" type="SystemEx.IO.Stream">
        /// </param>
        for (var index = 0; index < n; index++) {
            var textureIndex = SystemEx.IO.SE.readInt32(textures);
            this.gl.deleteTexture(this._textures$3[textureIndex]);
            this._checkError$3('glDeleteTexture');
            this._textures$3[textureIndex] = null;
        }
    },
    
    depthFunc: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$depthFunc(func) {
        /// <param name="func" type="Number" integer="true">
        /// </param>
        this.gl.depthFunc(func);
        this._checkError$3('glDepthFunc');
    },
    
    depthMask: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$depthMask(flag) {
        /// <param name="flag" type="Boolean">
        /// </param>
        this.gl.depthMask(flag);
        this._checkError$3('glDepthMask');
    },
    
    depthRangex: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$depthRangex(zNear, zFar) {
        /// <param name="zNear" type="Number" integer="true">
        /// </param>
        /// <param name="zFar" type="Number" integer="true">
        /// </param>
        this.gl.depthRange(zNear, zFar);
        this._checkError$3('glDepthRangex');
    },
    
    disable: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$disable(cap) {
        /// <param name="cap" type="Number" integer="true">
        /// </param>
        if (cap !== GLES20.texturE_2D) {
            this.gl.disable(cap);
            this._checkError$3('glDisable');
            return;
        }
        switch (this._activeTexture$3) {
            case 0:
                this.gl.uniform1i(this._uEnableTexture0$3, 0);
                break;
            case 1:
                this.gl.uniform1i(this._uEnableTexture1$3, 0);
                break;
            default:
                throw new Error('RuntimeException:');
        }
    },
    
    disableClientState: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$disableClientState(array) {
        /// <param name="array" type="Number" integer="true">
        /// </param>
        switch (array) {
            case GLES11.coloR_ARRAY:
                this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_COLOR);
                this._checkError$3('DisableClientState colorArr');
                break;
            case GLES11.verteX_ARRAY:
                this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_POSITION);
                this._checkError$3('DisableClientState vertexArrr');
                break;
            case GLES11.texturE_COORD_ARRAY:
                switch (this._clientActiveTexture$3) {
                    case 0:
                        this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_0);
                        this._checkError$3('DisableClientState texCoord0');
                        break;
                    case 1:
                        this.gl.disableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_1);
                        this._checkError$3('DisableClientState texCoord1');
                        break;
                    default:
                        throw new Error('RuntimeException:');
                }
                break;
            default:
                this._log$3('unsupported / unrecogized client state');
                break;
        }
    },
    
    drawArrays: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$drawArrays(mode, first, count) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        /// <param name="first" type="Number" integer="true">
        /// </param>
        /// <param name="count" type="Number" integer="true">
        /// </param>
        this._prepareDraw$3();
        this.gl.drawArrays(mode, first, count);
        this._checkError$3('drawArrays');
    },
    
    drawElements: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$drawElements(mode, count, type, indicies) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        /// <param name="count" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="indicies" type="SystemEx.IO.Stream">
        /// </param>
        this._prepareDraw$3();
        this.gl.bindBuffer(GLES20.elemenT_ARRAY_BUFFER, this._elementBuffer$3);
        this._checkError$3('bindBuffer(el)');
        this.gl.bufferData(GLES20.elemenT_ARRAY_BUFFER, this._getTypedArray$3(indicies, GLES20.unsigneD_SHORT), GLES20.dynamiC_DRAW);
        this._checkError$3('bufferData(el)');
        this.gl.drawElements(mode, count, GLES20.unsigneD_SHORT, 0);
        this._checkError$3('drawElements');
    },
    
    enable: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$enable(cap) {
        /// <param name="cap" type="Number" integer="true">
        /// </param>
        if (cap !== GLES20.texturE_2D) {
            this.gl.enable(cap);
            this._checkError$3('glEnable');
            return;
        }
        switch (this._activeTexture$3) {
            case 0:
                this.gl.uniform1i(this._uEnableTexture0$3, 1);
                break;
            case 1:
                this.gl.uniform1i(this._uEnableTexture1$3, 1);
                break;
            default:
                throw new Error('RuntimeException:');
        }
    },
    
    enableClientState: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$enableClientState(array) {
        /// <param name="array" type="Number" integer="true">
        /// </param>
        switch (array) {
            case GLES11.coloR_ARRAY:
                this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_COLOR);
                this._checkError$3('enableClientState colorArr');
                break;
            case GLES11.verteX_ARRAY:
                this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_POSITION);
                this._checkError$3('enableClientState vertexArrr');
                break;
            case GLES11.texturE_COORD_ARRAY:
                switch (this._clientActiveTexture$3) {
                    case 0:
                        this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_0);
                        this._checkError$3('enableClientState texCoord0');
                        break;
                    case 1:
                        this.gl.enableVertexAttribArray(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_1);
                        this._checkError$3('enableClientState texCoord1');
                        break;
                    default:
                        throw new Error('RuntimeException:');
                }
                break;
            default:
                this._log$3('unsupported / unrecogized client state ' + array);
                break;
        }
    },
    
    finish: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$finish() {
        this.gl.finish();
    },
    
    getError: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$getError() {
        /// <returns type="Number" integer="true"></returns>
        return this.gl.getError();
    },
    
    getString: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$getString(name) {
        /// <param name="name" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return 'glGetString not implemented';
    },
    
    pixelStorei: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pixelStorei(pname, param) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this.gl.pixelStorei(pname, param);
    },
    
    pointParameterx: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pointParameterx(pname, param) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
    },
    
    pointParameterxv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pointParameterxv(pname, pparams) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    pointSizex: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$pointSizex(size) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
    },
    
    readPixels: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$readPixels(x, y, width, height, format, type, pixels) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="pixels" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    scissor: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$scissor(x, y, width, height) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        this.gl.scissor(x, y, width, height);
        this._checkError$3('glScissor');
    },
    
    shadeModel: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$shadeModel(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
    },
    
    texCoordPointer: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texCoordPointer(size, type, stride, pointer) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        this._vertexAttribPointer$3(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_0 + this._clientActiveTexture$3, size, type, false, stride, pointer);
        this._checkError$3('texCoordPointer');
    },
    
    texEnvi: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texEnvi(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this._texEnvMode$3[this._activeTexture$3] = param;
    },
    
    texEnvx: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texEnvx(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this._texEnvMode$3[this._activeTexture$3] = param;
    },
    
    texEnviv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texEnviv(target, pname, pparams) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    texEnvxv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texEnvxv(target, pname, pparams) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    texImage2D: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texImage2D(target, level, internalformat, width, height, border, format, type, pixels) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="border" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="pixels" type="SystemEx.IO.Stream">
        /// </param>
        this._textureFormat$3[this._boundTextureId$3[this._activeTexture$3]] = internalformat;
        this.gl.texImage2D(target, level, internalformat, width, height, border, format, type, this._getTypedArray$3(pixels, type));
        this._checkError$3('glTexImage2D');
    },
    
    texImage2Di: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texImage2Di(target, level, internalformat, format, type, image) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="image" type="Object" domElement="true">
        /// </param>
        this.gl.texImage2D(target, level, internalformat, format, type, image);
        this._checkError$3('texImage2D');
    },
    
    texImage2De: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texImage2De(target, level, internalformat, format, type, canvas) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="canvas" type="Object" domElement="true">
        /// </param>
        this.gl.texImage2D(target, level, internalformat, format, type, canvas);
        this._checkError$3('texImage2D');
    },
    
    texImage2Dx: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texImage2Dx(target, level, internalformat, format, type, canvas) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="internalformat" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="canvas" type="Object" domElement="true">
        /// </param>
        this.gl.texImage2D(target, level, internalformat, format, type, canvas);
        this._checkError$3('texImage2D');
    },
    
    texParameteri: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texParameteri(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this.gl.texParameteri(target, pname, param);
        this._checkError$3('glTexParameteri');
    },
    
    texParameterx: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texParameterx(target, pname, param) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="param" type="Number" integer="true">
        /// </param>
        this.gl.texParameteri(target, pname, param);
        this._checkError$3('glTexParameteri');
    },
    
    texParameteriv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texParameteriv(target, pname, pparams) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    texParameterxv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texParameterxv(target, pname, pparams) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="pparams" type="SystemEx.IO.Stream">
        /// </param>
    },
    
    texSubImage2D: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        /// <param name="level" type="Number" integer="true">
        /// </param>
        /// <param name="xoffset" type="Number" integer="true">
        /// </param>
        /// <param name="yoffset" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="format" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="pixels" type="SystemEx.IO.Stream">
        /// </param>
        this.gl.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, this._getTypedArray$3(pixels, type));
        this._checkError$3('glTexSubImage2D');
    },
    
    vertexPointer: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$vertexPointer(size, type, stride, pointer) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        this._vertexAttribPointer$3(SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_POSITION, size, type, false, stride, pointer);
        this._checkError$3('glVertexPointer');
    },
    
    viewport: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$viewport(x, y, width, height) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl.callBaseMethod(this, 'viewport', [ x, y, width, height ]);
        this.gl.viewport(x, y, width, height);
        this._checkError$3('glViewport');
    },
    
    generateMipmap: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$generateMipmap(target) {
        /// <param name="target" type="Number" integer="true">
        /// </param>
        this.gl.generateMipmap(target);
        this._checkError$3('genMipmap');
    },
    
    _polygonMode$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_polygonMode$3(i, j) {
        /// <param name="i" type="Number" integer="true">
        /// </param>
        /// <param name="j" type="Number" integer="true">
        /// </param>
    },
    
    _getTextureMode$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_getTextureMode$3(indx) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return ((this._texEnvMode$3[indx] === GLES20.REPLACE) ? 0 : ((this._textureFormats$3[this._boundTextureId$3[indx]] === 3) ? 2 : 1));
    },
    
    _updatTCBuffer$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_updatTCBuffer$3(s, offset, count) {
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="count" type="Number" integer="true">
        /// </param>
        var b = this._buffers$3[SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_0];
        this.gl.bindBuffer(GLES20.arraY_BUFFER, b.buffer);
        var p = s.get_position();
        var l = s.get_length();
        s.set_position(p + offset);
        s.setLength(p + offset + count);
        this.gl.bufferSubData(GLES20.arraY_BUFFER, offset * 4, this._getTypedArray$3(s, GLES20.FLOAT));
        s.set_position(p);
        s.setLength(l);
    },
    
    _prepareDraw$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_prepareDraw$3() {
        if (this.updateMvpMatrix()) {
            this.gl.uniformMatrix4fv(this._uMvpMatrix$3, false, this._mvpMatrix);
            this._checkError$3('prepareDraw');
        }
        this.gl.uniform1i(this._uTexEnv0$3, this._getTextureMode$3(0));
        this.gl.uniform1i(this._uTexEnv1$3, this._getTextureMode$3(1));
        for (var indx = 0; indx < SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl._smalL_BUF_COUNT$3; indx++) {
            var b = this._buffers$3[indx];
            if (b.toBind != null) {
                this.gl.bindBuffer(GLES20.arraY_BUFFER, b.buffer);
                this._checkError$3('bindBuffer' + indx);
                this.gl.bufferData(GLES20.arraY_BUFFER, b.toBind, GLES20.streaM_DRAW);
                this._checkError$3('bufferData' + indx);
                this.gl.vertexAttribPointer(indx, b.size, b.type, b.normalized, b.stride, 0);
                this._checkError$3('vertexAttribPointer');
                b.toBind = null;
            }
        }
    },
    
    _vertexAttribPointer$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_vertexAttribPointer$3(indx, size, type, normalize, stride, pointer) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="normalize" type="Boolean">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        var b = this._buffers$3[indx];
        b.stride = stride;
        b.size = size;
        b.normalized = normalize;
        b.type = type;
        b.toBind = this._getTypedArray$3(pointer, type);
    },
    
    _vertexAttribStaticDrawPointer$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_vertexAttribStaticDrawPointer$3(indx, size, type, normalized, stride, offset, pointer, staticDrawIndex) {
        /// <param name="indx" type="Number" integer="true">
        /// </param>
        /// <param name="size" type="Number" integer="true">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <param name="normalized" type="Boolean">
        /// </param>
        /// <param name="stride" type="Number" integer="true">
        /// </param>
        /// <param name="offset" type="Number" integer="true">
        /// </param>
        /// <param name="pointer" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="staticDrawIndex" type="Number" integer="true">
        /// </param>
        var b = this._staticDrawBuffers$3[staticDrawIndex];
        if (b == null) {
            this._staticDrawBuffers$3[staticDrawIndex] = b = this.gl.createBuffer();
            this.gl.bindBuffer(GLES20.arraY_BUFFER, b);
            this.gl.bufferData(GLES20.arraY_BUFFER, this._getTypedArray$3(pointer, type), GLES20.statiC_DRAW);
            this._checkError$3('bufferData');
        }
        this.gl.bindBuffer(GLES20.arraY_BUFFER, b);
        this.gl.vertexAttribPointer(indx, size, type, normalized, stride, offset);
        this._checkError$3('vertexAttribPointer');
        this._buffers$3[indx].toBind = null;
    },
    
    _getTypedArray$3: function SystemEx_Interop_OpenGL_WebGLES11RenderingContextImpl$_getTypedArray$3(s, type) {
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        /// <param name="type" type="Number" integer="true">
        /// </param>
        /// <returns type="ArrayBufferView"></returns>
        var memoryStream = (Type.safeCast(s, SystemEx.IO.MemoryStream));
        var array = null;
        var remainingBytes = (s.get_length() - s.get_position());
        var byteOffset = 0;
        switch (type) {
            case GLES20.FLOAT:
                return new Float32Array(array.buffer, byteOffset, remainingBytes / 4);
            case GLES20.unsigneD_BYTE:
                return new Uint8Array(array.buffer, byteOffset, remainingBytes);
            case GLES20.unsigneD_SHORT:
                return new Uint16Array(array.buffer, byteOffset, remainingBytes / 2);
            case GLES20.INT:
                return new Int32Array(array.buffer, byteOffset, remainingBytes / 4);
            case GLES20.SHORT:
                return new Int16Array(array.buffer, byteOffset, remainingBytes / 2);
            case GLES20.BYTE:
                return new Int8Array(array.buffer, byteOffset, remainingBytes);
        }
        throw new Error('IllegalArgumentException:');
    }
}


////////////////////////////////////////////////////////////////////////////////
// SystemEx.Interop.OpenGL.WebGLES11RenderingContext

SystemEx.Interop.OpenGL.WebGLES11RenderingContext = function SystemEx_Interop_OpenGL_WebGLES11RenderingContext(width, height) {
    /// <param name="width" type="Number" integer="true">
    /// </param>
    /// <param name="height" type="Number" integer="true">
    /// </param>
    /// <field name="arraY_POSITION" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_COLOR" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_TEXCOORD_0" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="arraY_TEXCOORD_1" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_matrixMode$2" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportX$2" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportY$2" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportW$2" type="Number" integer="true">
    /// </field>
    /// <field name="_viewportH$2" type="Number" integer="true">
    /// </field>
    /// <field name="_projectionMatrix$2" type="Array" elementType="Number">
    /// </field>
    /// <field name="_modelViewMatrix$2" type="Array" elementType="Number">
    /// </field>
    /// <field name="_textureMatrix$2" type="Array" elementType="Number">
    /// </field>
    /// <field name="_currentMatrix$2" type="Array" elementType="Number">
    /// </field>
    /// <field name="_projectionMatrixStack$2" type="Array">
    /// </field>
    /// <field name="_modelViewMatrixStack$2" type="Array">
    /// </field>
    /// <field name="_textureMatrixStack$2" type="Array">
    /// </field>
    /// <field name="_currentMatrixStack$2" type="Array">
    /// </field>
    /// <field name="_tmpMatrix$2" type="Array" elementType="Number">
    /// </field>
    /// <field name="_mvpMatrix" type="Array" elementType="Number">
    /// </field>
    /// <field name="_mvpDirty$2" type="Boolean">
    /// </field>
    /// <field name="_width$2" type="Number" integer="true">
    /// </field>
    /// <field name="_height$2" type="Number" integer="true">
    /// </field>
    this._matrixMode$2 = GLES11.MODELVIEW;
    this._projectionMatrix$2 = new Array(16);
    this._modelViewMatrix$2 = new Array(16);
    this._textureMatrix$2 = new Array(16);
    this._projectionMatrixStack$2 = [];
    this._modelViewMatrixStack$2 = [];
    this._textureMatrixStack$2 = [];
    this._tmpMatrix$2 = new Array(16);
    this._mvpMatrix = new Array(16);
    SystemEx.Interop.OpenGL.WebGLES11RenderingContext.initializeBase(this);
    SystemEx.MathMatrix.setIdentityM(this._modelViewMatrix$2, 0);
    SystemEx.MathMatrix.setIdentityM(this._projectionMatrix$2, 0);
    SystemEx.MathMatrix.setIdentityM(this._textureMatrix$2, 0);
    this._width$2 = width;
    this._height$2 = height;
}
SystemEx.Interop.OpenGL.WebGLES11RenderingContext.prototype = {
    _viewportX$2: 0,
    _viewportY$2: 0,
    _viewportW$2: 0,
    _viewportH$2: 0,
    _currentMatrix$2: null,
    _currentMatrixStack$2: null,
    _mvpDirty$2: true,
    _width$2: 0,
    _height$2: 0,
    
    alphaFunc: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$alphaFunc(func, r) {
        /// <param name="func" type="Number" integer="true">
        /// </param>
        /// <param name="r" type="Number">
        /// </param>
    },
    
    frustumf: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$frustumf(left, right, bottom, top, zNear, zFar) {
        /// <param name="left" type="Number">
        /// </param>
        /// <param name="right" type="Number">
        /// </param>
        /// <param name="bottom" type="Number">
        /// </param>
        /// <param name="top" type="Number">
        /// </param>
        /// <param name="zNear" type="Number">
        /// </param>
        /// <param name="zFar" type="Number">
        /// </param>
        var a = 2 * zNear;
        var b = right - left;
        var c = top - bottom;
        var d = zFar - zNear;
        var matrix = [ (a / b), 0, 0, 0, 0, (a / c), 0, 0, ((right + left) / b), ((top + bottom) / c), ((-zFar - zNear) / d), -1, 0, 0, ((-a * zFar) / d), 0 ];
        this.multMatrixf(matrix);
    },
    
    getFloatv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$getFloatv(name, s) {
        /// <param name="name" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        switch (name) {
            case GLES11.MODELVIEW:
            case GLES11.modelvieW_MATRIX:
                var p = s.get_position();
                for (var index = 0; index < this._modelViewMatrix$2.length; index++) {
                    SystemEx.IO.SE.writeSingle(s, this._modelViewMatrix$2[index]);
                }
                s.set_position(p);
                break;
            default:
                throw new Error('ArgumentException: glGetFloat');
        }
    },
    
    loadMatrixf: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$loadMatrixf(s) {
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        var p = s.get_position();
        for (var index = 0; index < this._currentMatrix$2.length; index++) {
            this._currentMatrix$2[index] = SystemEx.IO.SE.readSingle(s);
        }
        s.set_position(p);
        this._mvpDirty$2 = true;
    },
    
    multMatrixf: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$multMatrixf(m) {
        /// <param name="m" type="Array" elementType="Number">
        /// </param>
        var ofs = 0;
        SystemEx.MathMatrix.multiplyMM(this._tmpMatrix$2, 0, this._currentMatrix$2, 0, m, ofs);
        SystemEx.JSArrayEx.copy(this._tmpMatrix$2, 0, this._currentMatrix$2, 0, 16);
        this._mvpDirty$2 = true;
    },
    
    orthof: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$orthof(left, right, bottom, top, zNear, zFar) {
        /// <param name="left" type="Number">
        /// </param>
        /// <param name="right" type="Number">
        /// </param>
        /// <param name="bottom" type="Number">
        /// </param>
        /// <param name="top" type="Number">
        /// </param>
        /// <param name="zNear" type="Number">
        /// </param>
        /// <param name="zFar" type="Number">
        /// </param>
        var matrix = [ 2 / (right - left), 0, 0, 0, 0, 2 / (top - bottom), 0, 0, 0, 0, -2 / zFar - zNear, 0, -(right + left) / (right - left), -(top + bottom) / (top - bottom), -(zFar + zNear) / (zFar - zNear), 1 ];
        this.multMatrixf(matrix);
        this._mvpDirty$2 = true;
    },
    
    rotatef: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$rotatef(angle, x, y, z) {
        /// <param name="angle" type="Number">
        /// </param>
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        if ((x !== 0) || (y !== 0) || (z !== 0)) {
            SystemEx.MathMatrix.rotateM2(this._currentMatrix$2, 0, angle, x, y, z);
        }
        this._mvpDirty$2 = true;
    },
    
    scalef: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$scalef(x, y, z) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        SystemEx.MathMatrix.scaleM2(this._currentMatrix$2, 0, x, y, z);
        this._mvpDirty$2 = true;
    },
    
    translatef: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$translatef(x, y, z) {
        /// <param name="x" type="Number">
        /// </param>
        /// <param name="y" type="Number">
        /// </param>
        /// <param name="z" type="Number">
        /// </param>
        SystemEx.MathMatrix.translateM2(this._currentMatrix$2, 0, x, y, z);
        this._mvpDirty$2 = true;
    },
    
    alphaFuncx: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$alphaFuncx(func, r) {
        /// <param name="func" type="Number" integer="true">
        /// </param>
        /// <param name="r" type="Number" integer="true">
        /// </param>
    },
    
    getIntegerv: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$getIntegerv(pname, s) {
        /// <param name="pname" type="Number" integer="true">
        /// </param>
        /// <param name="s" type="SystemEx.IO.Stream">
        /// </param>
        switch (pname) {
            case GLES11.matriX_MODE:
                SystemEx.IO.SE.writeUInt32(s, this._matrixMode$2);
                break;
            default:
                throw new Error('ArgumentException:');
        }
    },
    
    loadIdentity: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$loadIdentity() {
        SystemEx.MathMatrix.setIdentityM(this._currentMatrix$2, 0);
        this._mvpDirty$2 = true;
    },
    
    loadMatrixx: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$loadMatrixx(m) {
        /// <param name="m" type="SystemEx.IO.Stream">
        /// </param>
        var p = m.get_position();
        for (var index = 0; index < this._currentMatrix$2.length; index++) {
            this._currentMatrix$2[index] = SystemEx.IO.SE.readUInt32(m);
        }
        m.set_position(p);
        this._mvpDirty$2 = true;
    },
    
    matrixMode: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$matrixMode(mode) {
        /// <param name="mode" type="Number" integer="true">
        /// </param>
        switch (mode) {
            case GLES11.MODELVIEW:
                this._currentMatrix$2 = this._modelViewMatrix$2;
                this._currentMatrixStack$2 = this._modelViewMatrixStack$2;
                break;
            case GLES11.PROJECTION:
                this._currentMatrix$2 = this._projectionMatrix$2;
                this._currentMatrixStack$2 = this._projectionMatrixStack$2;
                break;
            case GLES20.TEXTURE:
                this._currentMatrix$2 = this._textureMatrix$2;
                this._currentMatrixStack$2 = this._textureMatrixStack$2;
                break;
            default:
                throw new Error('ArgumentException: Unrecoginzed matrix mode: ' + mode);
        }
        this._matrixMode$2 = mode;
    },
    
    orthox: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$orthox(left, right, bottom, top, zNear, zFar) {
        /// <param name="left" type="Number" integer="true">
        /// </param>
        /// <param name="right" type="Number" integer="true">
        /// </param>
        /// <param name="bottom" type="Number" integer="true">
        /// </param>
        /// <param name="top" type="Number" integer="true">
        /// </param>
        /// <param name="zNear" type="Number" integer="true">
        /// </param>
        /// <param name="zFar" type="Number" integer="true">
        /// </param>
        var l = left;
        var r = right;
        var b = bottom;
        var n = zNear;
        var f = zFar;
        var t = top;
        var matrix = [ 2 / (r - l), 0, 0, 0, 0, 2 / (t - b), 0, 0, 0, 0, -2 / f - n, 0, -(r + l) / (r - l), -(t + b) / (t - b), -(f + n) / (f - n), 1 ];
        this.multMatrixf(matrix);
        this._mvpDirty$2 = true;
    },
    
    popMatrix: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$popMatrix() {
        var top = this._currentMatrixStack$2.pop();
        SystemEx.JSArrayEx.copy(top, 0, this._currentMatrix$2, 0, 16);
        this._mvpDirty$2 = true;
    },
    
    pushMatrix: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$pushMatrix() {
        var copy = new Array(16);
        SystemEx.JSArrayEx.copy(this._currentMatrix$2, 0, copy, 0, 16);
        this._currentMatrixStack$2.push(copy);
    },
    
    scalex: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$scalex(x, y, z) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="z" type="Number" integer="true">
        /// </param>
        SystemEx.MathMatrix.scaleM2(this._currentMatrix$2, 0, x, y, z);
        this._mvpDirty$2 = true;
    },
    
    translatex: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$translatex(x, y, z) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="z" type="Number" integer="true">
        /// </param>
        SystemEx.MathMatrix.translateM2(this._currentMatrix$2, 0, x, y, z);
        this._mvpDirty$2 = true;
    },
    
    viewport: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$viewport(x, y, width, height) {
        /// <param name="x" type="Number" integer="true">
        /// </param>
        /// <param name="y" type="Number" integer="true">
        /// </param>
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        this._viewportX$2 = x;
        this._viewportY$2 = y;
        this._viewportW$2 = width;
        this._viewportH$2 = height;
    },
    
    updateMvpMatrix: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$updateMvpMatrix() {
        /// <returns type="Boolean"></returns>
        if (!this._mvpDirty$2) {
            return false;
        }
        SystemEx.MathMatrix.multiplyMM(this._mvpMatrix, 0, this._projectionMatrix$2, 0, this._modelViewMatrix$2, 0);
        this._mvpDirty$2 = false;
        return true;
    },
    
    project: function SystemEx_Interop_OpenGL_WebGLES11RenderingContext$project(objX, objY, objZ, view, win) {
        /// <param name="objX" type="Number">
        /// </param>
        /// <param name="objY" type="Number">
        /// </param>
        /// <param name="objZ" type="Number">
        /// </param>
        /// <param name="view" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <param name="win" type="Array" elementType="Number">
        /// </param>
        /// <returns type="Boolean"></returns>
        var v = [ objX, objY, objZ, 1 ];
        var v2 = new Array(4);
        SystemEx.MathMatrix.multiplyMV(v2, 0, this._mvpMatrix, 0, v, 0);
        var w = v2[3];
        if (w === 0) {
            return false;
        }
        var rw = 1 / w;
        win[0] = this._viewportX$2 + this._viewportW$2 * (v2[0] * rw + 1) * 0.5;
        win[1] = this._viewportY$2 + this._viewportH$2 * (v2[1] * rw + 1) * 0.5;
        win[2] = (v2[2] * rw + 1) * 0.5;
        return true;
    }
}


SystemEx.Interop.OpenGL.WebGL15Context.registerClass('SystemEx.Interop.OpenGL.WebGL15Context');
SystemEx.Interop.OpenGL.WebGL15ContextImpl.registerClass('SystemEx.Interop.OpenGL.WebGL15ContextImpl', SystemEx.Interop.OpenGL.WebGL15Context);
SystemEx.Interop.OpenGL.WebGLES11RenderingContext.registerClass('SystemEx.Interop.OpenGL.WebGLES11RenderingContext', WebGLRenderingContext);
SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl.registerClass('SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl', SystemEx.Interop.OpenGL.WebGLES11RenderingContext);
SystemEx.Interop.OpenGL.WebGL15ContextImpl._smalL_BUF_COUNT$1 = 4;
SystemEx.Interop.OpenGL.WebGL15Context._begiN_END_MAX_VERTICES = 16384;
SystemEx.Interop.OpenGL.WebGL15Context.arraY_POSITION = 0;
SystemEx.Interop.OpenGL.WebGL15Context.arraY_COLOR = 1;
SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_0 = 2;
SystemEx.Interop.OpenGL.WebGL15Context.arraY_TEXCOORD_1 = 3;
SystemEx.Interop.OpenGL.WebGLES11RenderingContextImpl._smalL_BUF_COUNT$3 = 4;
SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_POSITION = 0;
SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_COLOR = 1;
SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_0 = 2;
SystemEx.Interop.OpenGL.WebGLES11RenderingContext.arraY_TEXCOORD_1 = 3;

}
ss.loader.registerScript('Script.WebGLES11', ['Script.WebEx'], executeScript);
})();

//! This script was generated using Script# v0.6.3.0
