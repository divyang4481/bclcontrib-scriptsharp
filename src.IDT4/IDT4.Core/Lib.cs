using System;
using IDT4.Math2;
namespace IDT4
{
    /// <summary>
    /// idLib contains stateless support classes and concrete types. Some classes do have static variables, but such variables are initialized once and
    /// read-only after initialization (they do not maintain a modifiable state).
    ///
    /// The interface pointers idSys, idCommon, idCVarSystem and idFileSystem should be set before using idLib. The pointers stored here should not
    /// be used by any part of the engine except for idLib.
    ///
    /// The frameNumber should be continuously set to the number of the current frame if frame base memory logging is required.
    /// </summary>
    public static class idLib
    {
        public static idSys sys;
        public static idCommon common;
        public static idCVarSystem cvarSystem;
        public static idFileSystem fileSystem;
        public static int frameNumber;

        public static void Init()
        {
            //// initialize little/big endian conversion
            //Swap_Init();
            //// initialize memory manager
            //Mem_Init();
            //// init string memory allocator
            //idStr.InitMemory();
            //// initialize generic SIMD implementation
            //idSIMD.Init();
            //// initialize math
            //idMath.Init();
            //// test idMatX
            ////idMatX::Test();
            //// test idPolynomial
            //idPolynomial.Test();
            //// initialize the dictionary string pools
            //idDict.Init();
        }

        public static void ShutDown()
        {
            //// shut down the dictionary string pools
            //idDict.Shutdown();
            //// shut down the string memory allocator
            //idStr.ShutdownMemory();
            //// shut down the SIMD engine
            //idSIMD.Shutdown();
            //// shut down the memory manager
            //Mem_Shutdown();
        }

        // wrapper to idCommon functions 
        public static void Error(string fmt, params object[] args)
        {
            string text = "";
            common.Error("%s", text);
        }

        public static void Warning(string fmt, params object[] args)
        {
            string text = "";
            common.Warning("%s", text);
        }

        // maximum world size
        public const int MAX_WORLD_COORD = (128 * 1024);
        public const int MIN_WORLD_COORD = (-128 * 1024);
        public const int MAX_WORLD_SIZE = (MAX_WORLD_COORD - MIN_WORLD_COORD);

        #region colors

        // basic colors
        public static readonly idVec4 colorBlack = new idVec4(0.00f, 0.00f, 0.00f, 1.00f);
        public static readonly idVec4 colorWhite = new idVec4(1.00f, 1.00f, 1.00f, 1.00f);
        public static readonly idVec4 colorRed = new idVec4(1.00f, 0.00f, 0.00f, 1.00f);
        public static readonly idVec4 colorGreen = new idVec4(0.00f, 1.00f, 0.00f, 1.00f);
        public static readonly idVec4 colorBlue = new idVec4(0.00f, 0.00f, 1.00f, 1.00f);
        public static readonly idVec4 colorYellow = new idVec4(1.00f, 1.00f, 0.00f, 1.00f);
        public static readonly idVec4 colorMagenta = new idVec4(1.00f, 0.00f, 1.00f, 1.00f);
        public static readonly idVec4 colorCyan = new idVec4(0.00f, 1.00f, 1.00f, 1.00f);
        public static readonly idVec4 colorOrange = new idVec4(1.00f, 0.50f, 0.00f, 1.00f);
        public static readonly idVec4 colorPurple = new idVec4(0.60f, 0.00f, 0.60f, 1.00f);
        public static readonly idVec4 colorPink = new idVec4(0.73f, 0.40f, 0.48f, 1.00f);
        public static readonly idVec4 colorBrown = new idVec4(0.40f, 0.35f, 0.08f, 1.00f);
        public static readonly idVec4 colorLtGrey = new idVec4(0.75f, 0.75f, 0.75f, 1.00f);
        public static readonly idVec4 colorMdGrey = new idVec4(0.50f, 0.50f, 0.50f, 1.00f);
        public static readonly idVec4 colorDkGrey = new idVec4(0.25f, 0.25f, 0.25f, 1.00f);

        static readonly uint[] _colorMask = { 255, 0 };
        static byte ColorFloatToByte(float c) { return (byte)(((uint)(c * 255.0f)) & _colorMask[idMath.FLOATSIGNBITSET(c) ? 1 : 0]); }

        // packs color floats in the range [0,1] into an integer
        static uint PackColor(ref idVec3 color)
        {
            uint dx = ColorFloatToByte(color.x);
            uint dy = ColorFloatToByte(color.y);
            uint dz = ColorFloatToByte(color.z);
            uint dw = ColorFloatToByte(color.w);
            return (dx << 0) | (dy << 8) | (dz << 16) | (dw << 24);
        }

        static void UnpackColor(uint color, ref idVec4 unpackedColor)
        {
            unpackedColor.Set(((color >> 0) & 255) * (1.0f / 255.0f),
                ((color >> 8) & 255) * (1.0f / 255.0f),
                ((color >> 16) & 255) * (1.0f / 255.0f),
                ((color >> 24) & 255) * (1.0f / 255.0f));

        }

        static uint PackColor(ref idVec4 color)
        {
            uint dx = ColorFloatToByte(color.x);
            uint dy = ColorFloatToByte(color.y);
            uint dz = ColorFloatToByte(color.z);
            return (dx << 0) | (dy << 8) | (dz << 16);
        }

        static void UnpackColor(uint color, ref idVec3 unpackedColor)
        {
            unpackedColor.Set(((color >> 0) & 255) * (1.0f / 255.0f),
                ((color >> 8) & 255) * (1.0f / 255.0f),
                ((color >> 16) & 255) * (1.0f / 255.0f));
        }

        #endregion

        #region little/big endian conversion

        public static short BigShort(short l) { return _BigShort(l); }
        public static short LittleShort(short l) { return _LittleShort(l); }
        public static int BigLong(int l) { return _BigLong(l); }
        public static int LittleLong(int l) { return _LittleLong(l); }
        public static float BigFloat(float l) { return _BigFloat(l); }
        public static float LittleFloat(float l) { return _LittleFloat(l); }
        public static void BigRevBytes(byte[] bp, int elsize, int elcount) { _BigRevBytes(bp, elsize, elcount); }
        public static void LittleRevBytes(byte[] bp, int elsize, int elcount) { _LittleRevBytes(bp, elsize, elcount); }
        public static void LittleBitField(byte[] bp, int elsize) { _LittleBitField(bp, elsize); }
        public static void Swap_Init()
        {
            byte[] swaptest = { 1, 0 };
            // set the byte swapping variables in a portable manner	
            if ((short)swaptest == 1)
            {
                // little endian ex: x86
                _BigShort = ShortSwap;
                _LittleShort = ShortNoSwap;
                _BigLong = LongSwap;
                _LittleLong = LongNoSwap;
                _BigFloat = FloatSwap;
                _LittleFloat = FloatNoSwap;
                _BigRevBytes = RevBytesSwap;
                _LittleRevBytes = RevBytesNoSwap;
                _LittleBitField = RevBitFieldNoSwap;
                _SixtetsForInt = SixtetsForIntLittle;
                _IntForSixtets = IntForSixtetsLittle;
            }
            else
            {
                // big endian ex: ppc
                _BigShort = ShortNoSwap;
                _LittleShort = ShortSwap;
                _BigLong = LongNoSwap;
                _LittleLong = LongSwap;
                _BigFloat = FloatNoSwap;
                _LittleFloat = FloatSwap;
                _BigRevBytes = RevBytesNoSwap;
                _LittleRevBytes = RevBytesSwap;
                _LittleBitField = RevBitFieldSwap;
                _SixtetsForInt = SixtetsForIntBig;
                _IntForSixtets = IntForSixtetsBig;
            }
        }
        public static bool Swap_IsBigEndian()
        {
            byte[] swaptest = { 1, 0 };
            return (short)swaptest != 1;
        }
        // for base64
        public static void SixtetsForInt(byte[] @out, int src) { _SixtetsForInt(@out, src); }
        public static int IntForSixtets(byte[] @in) { return _IntForSixtets(@in); }

        static Func<short, short> _BigShort;
        static Func<short, short> _LittleShort;
        static Func<int, int> _BigLong;
        static Func<int, int> _LittleLong;
        static Func<float, float> _BigFloat;
        static Func<float, float> _LittleFloat;
        static Action<byte[], int, int> _BigRevBytes;
        static Action<byte[], int, int> _LittleRevBytes;
        static Action<byte[], int> _LittleBitField;
        static Action<byte[], int> _SixtetsForInt;
        static Func<byte[], int> _IntForSixtets;

        static short ShortSwap(short l)
        {
            //byte b1, b2;
            //b1 = l & 255;
            //b2 = (l >> 8) & 255;
            //return (b1 << 8) + b2;
            return l;
        }

        static short ShortNoSwap(short l) { return l; }

        static int LongSwap(int l)
        {
            //byte b1, b2, b3, b4;
            //b1 = l & 255;
            //b2 = (l >> 8) & 255;
            //b3 = (l >> 16) & 255;
            //b4 = (l >> 24) & 255;
            //return ((int)b1 << 24) + ((int)b2 << 16) + ((int)b3 << 8) + b4;
            return l;
        }

        static int LongNoSwap(int l) { return l; }

        static float FloatSwap(float f)
        {
            //union {
            //    float	f;
            //    byte	b[4];
            //} dat1, dat2;
            //dat1.f = f;
            //dat2.b[0] = dat1.b[3];
            //dat2.b[1] = dat1.b[2];
            //dat2.b[2] = dat1.b[1];
            //dat2.b[3] = dat1.b[0];
            //return dat2.f;
            return f;
        }

        static float FloatNoSwap(float f) { return f; }

        static void RevBytesSwap(byte[] bp, int elsize, int elcount)
        {
            //register unsigned char *p, *q;
            //p = ( unsigned char * ) bp;
            //if ( elsize == 2 ) {
            //    q = p + 1;
            //    while ( elcount-- ) {
            //        *p ^= *q;
            //        *q ^= *p;
            //        *p ^= *q;
            //        p += 2;
            //        q += 2;
            //    }
            //    return;
            //}
            //while ( elcount-- ) {
            //    q = p + elsize - 1;
            //    while ( p < q ) {
            //        *p ^= *q;
            //        *q ^= *p;
            //        *p ^= *q;
            //        ++p;
            //        --q;
            //    }
            //    p += elsize >> 1;
            //}
        }

        static void RevBitFieldSwap(byte[] bp, int elsize)
        {
            //int i;
            //unsigned char *p, t, v;
            //LittleRevBytes( bp, elsize, 1 );
            //p = (unsigned char *) bp;
            //while ( elsize-- ) {
            //    v = *p;
            //    t = 0;
            //    for (i = 7; i; i--) {
            //        t <<= 1;
            //        v >>= 1;
            //        t |= v & 1;
            //    }
            //    *p++ = t;
            //}
        }

        static void RevBytesNoSwap(byte[] bp, int elsize, int elcount) { }
        static void RevBitFieldNoSwap(byte[] bp, int elsize) { }

        static void SixtetsForIntLittle(byte[] @out, int src)
        {
            byte[] b = (byte[])src;
            @out[0] = (b[0] & 0xfc) >> 2;
            @out[1] = ((b[0] & 0x3) << 4) + ((b[1] & 0xf0) >> 4);
            @out[2] = ((b[1] & 0xf) << 2) + ((b[2] & 0xc0) >> 6);
            @out[3] = b[2] & 0x3f;
        }

        static void SixtetsForIntBig(byte[] @out, int src)
        {
            for (int i = 0; i < 4; i++)
            {
                @out[i] = src & 0x3f;
                src >>= 6;
            }
        }

        static int IntForSixtetsLittle(byte[] @in)
        {
            int ret = 0;
            byte[] b = (byte[])ret;
            b[0] |= @in[0] << 2;
            b[0] |= (@in[1] & 0x30) >> 4;
            b[1] |= (@in[1] & 0xf) << 4;
            b[1] |= (@in[2] & 0x3c) >> 2;
            b[2] |= (@in[2] & 0x3) << 6;
            b[2] |= @in[3];
            return ret;
        }

        static int IntForSixtetsBig(byte[] @in)
        {
            int ret = 0;
            ret |= @in[0];
            ret |= @in[1] << 6;
            ret |= @in[2] << 2 * 6;
            ret |= @in[3] << 3 * 6;
            return ret;
        }

        #endregion
    }

    //typedef unsigned char			byte;		// 8 bits
    //typedef unsigned short			word;		// 16 bits
    //typedef unsigned int			dword;		// 32 bits
    //#define BIT( num )				( 1 << ( num ) )

    public static class idMath
    {
        public static double SQRT_1OVER2;
        public static float M_RAD2DEG;
        public static bool FLOATSIGNBITSET(float x) { return false; }
        public static bool INTSIGNBITSET(int x) { return false; }

        internal static float Fabs(float len)
        {
            throw new NotImplementedException();
        }

        internal static float InvSqrt(float l)
        {
            throw new NotImplementedException();
        }

        internal static float RSqrt(float sqrLength)
        {
            throw new NotImplementedException();
        }

        internal static void SinCos(float theta, out float st, out float ct)
        {
            throw new NotImplementedException();
        }

        internal static float Sqrt(float len)
        {
            throw new NotImplementedException();
        }

        internal static float ATan16(float p, float cosom)
        {
            throw new NotImplementedException();
        }

        internal static float Sin16(float p)
        {
            throw new NotImplementedException();
        }

        internal static float ACos(float w)
        {
            throw new NotImplementedException();
        }
    }
}

//// memory management and arrays
//#include "Heap.h"
//#include "containers/List.h"

//// text manipulation
//#include "Str.h"
//#include "Token.h"
//#include "Lexer.h"
//#include "Parser.h"
//#include "Base64.h"
//#include "CmdArgs.h"

//// containers
//#include "containers/BTree.h"
//#include "containers/BinSearch.h"
//#include "containers/HashIndex.h"
//#include "containers/HashTable.h"
//#include "containers/StaticList.h"
//#include "containers/LinkList.h"
//#include "containers/Hierarchy.h"
//#include "containers/Queue.h"
//#include "containers/Stack.h"
//#include "containers/StrList.h"
//#include "containers/StrPool.h"
//#include "containers/VectorSet.h"
//#include "containers/PlaneSet.h"

//// hashing
//#include "hashing/CRC32.h"
//#include "hashing/MD4.h"
//#include "hashing/MD5.h"

//// misc
//#include "Dict.h"
//#include "LangDict.h"
//#include "BitMsg.h"
//#include "MapFile.h"
//#include "Timer.h"
