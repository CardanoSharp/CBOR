/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;
using System.IO;
using PeterO.Numbers;

namespace PeterO.Cbor {
  // Contains extra methods placed separately
  // because they are not CLS-compliant or they
  // are specific to the .NET framework.
  public sealed partial class CBORObject {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.AsUInt16"]/*'/>
    [CLSCompliant(false)]
    public ushort AsUInt16() {
      int v = this.AsInt32();
      if (v > UInt16.MaxValue || v < 0) {
        throw new OverflowException("This object's value is out of range");
      }
      return (ushort)v;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.AsUInt32"]/*'/>
    [CLSCompliant(false)]
    public uint AsUInt32() {
      ulong v = this.AsUInt64();
      if (v > UInt32.MaxValue) {
        throw new OverflowException("This object's value is out of range");
      }
      return (uint)v;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.AsSByte"]/*'/>
    [CLSCompliant(false)]
    public sbyte AsSByte() {
      int v = this.AsInt32();
      if (v > SByte.MaxValue || v < SByte.MinValue) {
        throw new OverflowException("This object's value is out of range");
      }
      return (sbyte)v;
    }

    private static decimal EncodeDecimal(
  EInteger bigmant,
  int scale,
  bool neg) {
      if (scale < 0) {
        throw new ArgumentException(
  "scale (" + scale + ") is less than 0");
      }
      if (scale > 28) {
        throw new ArgumentException(
  "scale (" + scale + ") is more than " + "28");
      }
      byte[] data = bigmant.ToBytes(true);
      var a = 0;
      var b = 0;
      var c = 0;
      for (var i = 0; i < Math.Min(4, data.Length); ++i) {
        a |= (((int)data[i]) & 0xff) << (i * 8);
      }
      for (int i = 4; i < Math.Min(8, data.Length); ++i) {
        b |= (((int)data[i]) & 0xff) << ((i - 4) * 8);
      }
      for (int i = 8; i < Math.Min(12, data.Length); ++i) {
        c |= (((int)data[i]) & 0xff) << ((i - 8) * 8);
      }
      int d = scale << 16;
      if (neg) {
        d |= 1 << 31;
      }
      return new Decimal(new[] { a, b, c, d });
    }

    private static readonly EInteger DecimalMaxValue = (EInteger.One <<
      96) - EInteger.One;

    private static readonly EInteger DecimalMinValue = -((EInteger.One <<
      96) - EInteger.One);

    private static EInteger DecimalToEInteger(decimal dec) {
      int[] bits = Decimal.GetBits(dec);
      var data = new byte[13];
      data[0] = (byte)(bits[0] & 0xff);
      data[1] = (byte)((bits[0] >> 8) & 0xff);
      data[2] = (byte)((bits[0] >> 16) & 0xff);
      data[3] = (byte)((bits[0] >> 24) & 0xff);
      data[4] = (byte)(bits[1] & 0xff);
      data[5] = (byte)((bits[1] >> 8) & 0xff);
      data[6] = (byte)((bits[1] >> 16) & 0xff);
      data[7] = (byte)((bits[1] >> 24) & 0xff);
      data[8] = (byte)(bits[2] & 0xff);
      data[9] = (byte)((bits[2] >> 8) & 0xff);
      data[10] = (byte)((bits[2] >> 16) & 0xff);
      data[11] = (byte)((bits[2] >> 24) & 0xff);
      data[12] = 0;
      int scale = (bits[3] >> 16) & 0xff;
      var bigint = EInteger.FromBytes(data, true);
      for (var i = 0; i < scale; ++i) {
        bigint /= (EInteger)10;
      }
      if ((bits[3] >> 31) != 0) {
        bigint = -bigint;
      }
      return bigint;
    }

    private static decimal ExtendedRationalToDecimal(ERational
      extendedNumber) {
      if (extendedNumber.IsInfinity() || extendedNumber.IsNaN()) {
        throw new OverflowException("This object's value is out of range");
      }
      try {
        EDecimal newDecimal = EDecimal.FromEInteger(extendedNumber.Numerator)
          .Divide(
  EDecimal.FromEInteger(extendedNumber.Denominator),
  EContext.CliDecimal.WithTraps(EContext.FlagOverflow));
        return EncodeDecimal(
  newDecimal.Mantissa.Abs(),
  -((int)newDecimal.Exponent),
  newDecimal.Mantissa.Sign < 0);
      } catch (ETrapException ex) {
        throw new OverflowException("This object's value is out of range", ex);
      }
    }

    private static decimal ExtendedDecimalToDecimal(EDecimal
      extendedNumber) {
      if (extendedNumber.IsInfinity() || extendedNumber.IsNaN()) {
        throw new OverflowException("This object's value is out of range");
      }
      try {
        EDecimal newDecimal = extendedNumber.RoundToPrecision(
          EContext.CliDecimal.WithTraps(EContext.FlagOverflow));
        return EncodeDecimal(
  newDecimal.Mantissa.Abs(),
  -((int)newDecimal.Exponent),
  newDecimal.Mantissa.Sign < 0);
      } catch (ETrapException ex) {
        throw new OverflowException("This object's value is out of range", ex);
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.AsDecimal"]/*'/>
    [CLSCompliant(false)]
    public decimal AsDecimal() {
      return (this.ItemType == CBORObjectTypeInteger) ?
        ((decimal)(long)this.ThisItem) : ((this.ItemType ==
        CBORObjectTypeExtendedRational) ?
        ExtendedRationalToDecimal((ERational)this.ThisItem) :
        ExtendedDecimalToDecimal(this.AsEDecimal()));
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.AsUInt64"]/*'/>
    [CLSCompliant(false)]
    public ulong AsUInt64() {
      ICBORNumber cn = NumberInterfaces[this.ItemType];
      if (cn == null) {
        throw new InvalidOperationException("Not a number type");
      }
      EInteger bigint = cn.AsBigInteger(this.ThisItem);
      if (bigint.Sign < 0 || bigint.GetSignedBitLength() > 64) {
        throw new OverflowException("This object's value is out of range");
      }
      byte[] data = bigint.ToBytes(true);
      var a = 0;
      var b = 0;
      for (var i = 0; i < Math.Min(4, data.Length); ++i) {
        a |= (((int)data[i]) & 0xff) << (i * 8);
      }
      for (int i = 4; i < Math.Min(8, data.Length); ++i) {
        b |= (((int)data[i]) & 0xff) << ((i - 4) * 8);
      }
      unchecked
      {
        var ret = (ulong)a;
        ret &= 0xffffffffL;
        var retb = (ulong)b;
        retb &= 0xffffffffL;
        ret |= retb << 32;
        return ret;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.Write(System.SByte,System.IO.Stream)"]/*'/>
    [CLSCompliant(false)]
    public static void Write(sbyte value, Stream stream) {
      Write((long)value, stream);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.Write(System.UInt64,System.IO.Stream)"]/*'/>
    [CLSCompliant(false)]
    public static void Write(ulong value, Stream stream) {
      if (stream == null) {
        throw new ArgumentNullException("stream");
      }
      if (value <= Int64.MaxValue) {
        Write((long)value, stream);
      } else {
        stream.WriteByte((byte)27);
        stream.WriteByte((byte)((value >> 56) & 0xff));
        stream.WriteByte((byte)((value >> 48) & 0xff));
        stream.WriteByte((byte)((value >> 40) & 0xff));
        stream.WriteByte((byte)((value >> 32) & 0xff));
        stream.WriteByte((byte)((value >> 24) & 0xff));
        stream.WriteByte((byte)((value >> 16) & 0xff));
        stream.WriteByte((byte)((value >> 8) & 0xff));
        stream.WriteByte((byte)(value & 0xff));
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.FromObject(System.Decimal)"]/*'/>
    public static CBORObject FromObject(decimal value) {
      int[] bits = Decimal.GetBits(value);
      int scale = (bits[3] >> 16) & 0xff;
      if (scale == 0 && Math.Round(value) == value) {
        // This is an integer
        return (value >= 0 && value <= UInt64.MaxValue) ?
          FromObject((ulong)value) : FromObject(DecimalToEInteger(value));
      }
      var data = new byte[13];
      data[0] = (byte)(bits[0] & 0xff);
      data[1] = (byte)((bits[0] >> 8) & 0xff);
      data[2] = (byte)((bits[0] >> 16) & 0xff);
      data[3] = (byte)((bits[0] >> 24) & 0xff);
      data[4] = (byte)(bits[1] & 0xff);
      data[5] = (byte)((bits[1] >> 8) & 0xff);
      data[6] = (byte)((bits[1] >> 16) & 0xff);
      data[7] = (byte)((bits[1] >> 24) & 0xff);
      data[8] = (byte)(bits[2] & 0xff);
      data[9] = (byte)((bits[2] >> 8) & 0xff);
      data[10] = (byte)((bits[2] >> 16) & 0xff);
      data[11] = (byte)((bits[2] >> 24) & 0xff);
      data[12] = 0;
      var mantissa = EInteger.FromBytes(data, true);
      bool negative = (bits[3] >> 31) != 0;
      if (negative) {
        mantissa = -mantissa;
      }
      return FromObjectAndTag(
  new[] { FromObject(-scale),
        FromObject(mantissa) },
 4);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.Write(System.UInt32,System.IO.Stream)"]/*'/>
    [CLSCompliant(false)]
    public static void Write(uint value, Stream stream) {
      Write((ulong)value, stream);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.Write(System.UInt16,System.IO.Stream)"]/*'/>
    [CLSCompliant(false)]
    public static void Write(ushort value, Stream stream) {
      Write((ulong)value, stream);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.FromObject(System.SByte)"]/*'/>
    [CLSCompliant(false)]
    public static CBORObject FromObject(sbyte value) {
      return FromObject((long)value);
    }

    private static EInteger UInt64ToEInteger(ulong value) {
      var data = new byte[9];
      ulong uvalue = value;
      data[0] = (byte)(uvalue & 0xff);
      data[1] = (byte)((uvalue >> 8) & 0xff);
      data[2] = (byte)((uvalue >> 16) & 0xff);
      data[3] = (byte)((uvalue >> 24) & 0xff);
      data[4] = (byte)((uvalue >> 32) & 0xff);
      data[5] = (byte)((uvalue >> 40) & 0xff);
      data[6] = (byte)((uvalue >> 48) & 0xff);
      data[7] = (byte)((uvalue >> 56) & 0xff);
      data[8] = (byte)0;
      return EInteger.FromBytes(data, true);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.FromObject(System.UInt64)"]/*'/>
    [CLSCompliant(false)]
    public static CBORObject FromObject(ulong value) {
      return CBORObject.FromObject(new BigInteger(
        UInt64ToEInteger(value)));
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.FromObject(System.UInt32)"]/*'/>
    [CLSCompliant(false)]
    public static CBORObject FromObject(uint value) {
      return FromObject((long)(Int64)value);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.FromObject(System.UInt16)"]/*'/>
    [CLSCompliant(false)]
    public static CBORObject FromObject(ushort value) {
      return FromObject((long)(Int64)value);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.FromObjectAndTag(System.Object,System.UInt64)"]/*'/>
    [CLSCompliant(false)]
    public static CBORObject FromObjectAndTag(Object o, ulong tag) {
      return FromObjectAndTag(o, UInt64ToEInteger(tag));
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.op_Addition(PeterO.Cbor.CBORObject,PeterO.Cbor.CBORObject)"]/*'/>
    public static CBORObject operator +(CBORObject a, CBORObject b) {
      return Addition(a, b);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.op_Subtraction(PeterO.Cbor.CBORObject,PeterO.Cbor.CBORObject)"]/*'/>
    public static CBORObject operator -(CBORObject a, CBORObject b) {
      return Subtract(a, b);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.op_Multiply(PeterO.Cbor.CBORObject,PeterO.Cbor.CBORObject)"]/*'/>
    public static CBORObject operator *(CBORObject a, CBORObject b) {
      return Multiply(a, b);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.op_Division(PeterO.Cbor.CBORObject,PeterO.Cbor.CBORObject)"]/*'/>
    public static CBORObject operator /(CBORObject a, CBORObject b) {
      return Divide(a, b);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Cbor.CBORObject.op_Modulus(PeterO.Cbor.CBORObject,PeterO.Cbor.CBORObject)"]/*'/>
    public static CBORObject operator %(CBORObject a, CBORObject b) {
      return Remainder(a, b);
    }
  }
}
