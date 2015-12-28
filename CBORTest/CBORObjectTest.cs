using System;
using System.IO;
using NUnit.Framework;
using PeterO;
using PeterO.Cbor;

namespace Test {
  [TestFixture]
  public class CBORObjectTest {
    [Test]
    public void TestAbs() {
      Assert.AreEqual(
        CBORObject.FromObject(2),
        CBORObject.FromObject(-2).Abs());
      Assert.AreEqual(
        CBORObject.FromObject(2),
        CBORObject.FromObject(2).Abs());
      Assert.AreEqual(
        CBORObject.FromObject(2.5),
        CBORObject.FromObject(-2.5).Abs());
      Assert.AreEqual(
        CBORObject.FromObject(ExtendedDecimal.FromString("6.63")),
        CBORObject.FromObject(ExtendedDecimal.FromString("-6.63")).Abs());
      Assert.AreEqual(
        CBORObject.FromObject(ExtendedFloat.FromString("2.75")),
        CBORObject.FromObject(ExtendedFloat.FromString("-2.75")).Abs());
      Assert.AreEqual(
        CBORObject.FromObject(ExtendedRational.FromDouble(2.5)),
        CBORObject.FromObject(ExtendedRational.FromDouble(-2.5)).Abs());
    }
    [Test]
    public void TestAdd() {
      // not implemented yet
    }
    [Test]
    public void TestAddConverter() {
      // not implemented yet
    }
    [Test]
    public void TestAddition() {
      try {
        CBORObject.Addition(null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Addition(CBORObject.FromObject(2), null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Addition(CBORObject.Null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Addition(CBORObject.FromObject(2), CBORObject.Null);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestAddTagHandler() {
      // not implemented yet
    }
    [Test]
    public void TestAsBigInteger() {
      try {
        CBORObject.FromObject((object)null).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Null.AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewArray().AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        string numberString = numberinfo["number"].AsString();
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberString));
        if (!numberinfo["integer"].Equals(CBORObject.Null)) {
          Assert.AreEqual(
            numberinfo["integer"].AsString(),
            cbornumber.AsBigInteger().ToString());
        } else {
          try {
            cbornumber.AsBigInteger();
            Assert.Fail("Should have failed");
          } catch (OverflowException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }

      {
        string stringTemp =
          CBORObject.FromObject((float)0.75).AsBigInteger().ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((float)0.99).AsBigInteger().ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((float)0.0000000000000001).AsBigInteger()
                .ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((float)0.5).AsBigInteger().ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((float)1.5).AsBigInteger().ToString();
        Assert.AreEqual(
        "1",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((float)2.5).AsBigInteger().ToString();
        Assert.AreEqual(
        "2",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((float)328323f).AsBigInteger().ToString();
        Assert.AreEqual(
        "328323",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)0.75).AsBigInteger().ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)0.99).AsBigInteger().ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)0.0000000000000001).AsBigInteger()
                .ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)0.5).AsBigInteger().ToString();
        Assert.AreEqual(
        "0",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)1.5).AsBigInteger().ToString();
        Assert.AreEqual(
        "1",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)2.5).AsBigInteger().ToString();
        Assert.AreEqual(
        "2",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject((double)328323).AsBigInteger().ToString();
        Assert.AreEqual(
        "328323",
        stringTemp);
      }
      try {
        CBORObject.FromObject(Single.PositiveInfinity).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString()); throw new
          InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(Single.NegativeInfinity).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString()); throw new
          InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(Single.NaN).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString()); throw new
          InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(Double.PositiveInfinity).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString()); throw new
          InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(Double.NegativeInfinity).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString()); throw new
          InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(Double.NaN).AsBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString()); throw new
          InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestAsBoolean() {
      Assert.IsTrue(CBORObject.True.AsBoolean());
      Assert.IsTrue(CBORObject.FromObject(0).AsBoolean());
      Assert.IsTrue(CBORObject.FromObject(String.Empty).AsBoolean());
      Assert.IsFalse(CBORObject.False.AsBoolean());
      Assert.IsFalse(CBORObject.Null.AsBoolean());
      Assert.IsFalse(CBORObject.Undefined.AsBoolean());
      Assert.IsTrue(CBORObject.NewArray().AsBoolean());
      Assert.IsTrue(CBORObject.NewMap().AsBoolean());
    }
    [Test]
    public void TestAsByte() {
      try {
        CBORObject.NewArray().AsByte();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsByte();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsByte();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsByte();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsByte();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsByte();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (numberinfo["byte"].AsBoolean()) {
          Assert.AreEqual(
    BigInteger.fromString(numberinfo["integer"].AsString()).intValueChecked(),
    ((int)cbornumber.AsByte()) & 0xff);
        } else {
          try {
            cbornumber.AsByte();
            Assert.Fail("Should have failed " + cbornumber);
          } catch (OverflowException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString() + cbornumber);
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }
    }

    private static void AreEqualExact(double a, double b) {
      if (Double.IsNaN(a)) {
        Assert.IsTrue(Double.IsNaN(b));
      } else if (a != b) {
        Assert.Fail("expected " + a + ", got " + b);
      }
    }

    private static void AreEqualExact(float a, float b) {
      if (Single.IsNaN(a)) {
        Assert.IsTrue(Single.IsNaN(b));
      } else if (a != b) {
        Assert.Fail("expected " + a + ", got " + b);
      }
    }

    [Test]
    public void TestAsDouble() {
      try {
        CBORObject.NewArray().AsDouble();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsDouble();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsDouble();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsDouble();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsDouble();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsDouble();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        AreEqualExact(
(double)ExtendedDecimal.FromString(numberinfo["number"].AsString()).ToDouble(),
cbornumber.AsDouble());
      }
    }
    [Test]
    public void TestAsExtendedDecimal() {
      Assert.AreEqual(
        ExtendedDecimal.PositiveInfinity,
        CBORObject.FromObject(Single.PositiveInfinity).AsExtendedDecimal());
      Assert.AreEqual(
        ExtendedDecimal.NegativeInfinity,
        CBORObject.FromObject(Single.NegativeInfinity).AsExtendedDecimal());
      Assert.IsTrue(CBORObject.FromObject(Single.NaN).AsExtendedDecimal()
                    .IsNaN());
      Assert.AreEqual(
        ExtendedDecimal.PositiveInfinity,
        CBORObject.FromObject(Double.PositiveInfinity).AsExtendedDecimal());
      Assert.AreEqual(
        ExtendedDecimal.NegativeInfinity,
        CBORObject.FromObject(Double.NegativeInfinity).AsExtendedDecimal());
      Assert.IsTrue(CBORObject.FromObject(Double.NaN).AsExtendedDecimal()
                    .IsNaN());
      try {
        CBORObject.NewArray().AsExtendedDecimal();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsExtendedDecimal();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsExtendedDecimal();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsExtendedDecimal();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsExtendedDecimal();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsExtendedDecimal();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestAsExtendedFloat() {
      Assert.AreEqual(
        ExtendedFloat.PositiveInfinity,
        CBORObject.FromObject(Single.PositiveInfinity).AsExtendedFloat());
      Assert.AreEqual(
        ExtendedFloat.NegativeInfinity,
        CBORObject.FromObject(Single.NegativeInfinity).AsExtendedFloat());
      Assert.IsTrue(CBORObject.FromObject(Single.NaN).AsExtendedFloat()
                    .IsNaN());
      Assert.AreEqual(
        ExtendedFloat.PositiveInfinity,
        CBORObject.FromObject(Double.PositiveInfinity).AsExtendedFloat());
      Assert.AreEqual(
        ExtendedFloat.NegativeInfinity,
        CBORObject.FromObject(Double.NegativeInfinity).AsExtendedFloat());
      Assert.IsTrue(CBORObject.FromObject(Double.NaN).AsExtendedFloat()
                    .IsNaN());
    }
    [Test]
    public void TestAsExtendedRational() {
      Assert.AreEqual(
        ExtendedRational.PositiveInfinity,
        CBORObject.FromObject(Single.PositiveInfinity).AsExtendedRational());
      Assert.AreEqual(
        ExtendedRational.NegativeInfinity,
        CBORObject.FromObject(Single.NegativeInfinity).AsExtendedRational());
      Assert.IsTrue(CBORObject.FromObject(Single.NaN).AsExtendedRational()
                    .IsNaN());
      Assert.AreEqual(
        ExtendedRational.PositiveInfinity,
        CBORObject.FromObject(Double.PositiveInfinity).AsExtendedRational());
      Assert.AreEqual(
        ExtendedRational.NegativeInfinity,
        CBORObject.FromObject(Double.NegativeInfinity).AsExtendedRational());
      Assert.IsTrue(CBORObject.FromObject(Double.NaN).AsExtendedRational()
                    .IsNaN());
    }
    [Test]
    public void TestAsInt16() {
      try {
        CBORObject.NewArray().AsInt16();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsInt16();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsInt16();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsInt16();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsInt16();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsInt16();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(
            ExtendedDecimal.FromString(numberinfo["number"].AsString()));
        if (numberinfo["int16"].AsBoolean()) {
          Assert.AreEqual(
    BigInteger.fromString(numberinfo["integer"].AsString()).intValueChecked(),
    cbornumber.AsInt16());
        } else {
          try {
            cbornumber.AsInt16();
            Assert.Fail("Should have failed " + cbornumber);
          } catch (OverflowException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString() + cbornumber);
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }
    }

    [Test]
    public void TestAsInt32() {
      try {
        CBORObject.NewArray().AsInt32();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsInt32();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsInt32();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsInt32();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsInt32();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsInt32();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        ExtendedDecimal edec =
          ExtendedDecimal.FromString(numberinfo["number"].AsString());
        CBORObject cbornumber = CBORObject.FromObject(edec);
        bool isdouble = numberinfo["double"].AsBoolean();
        CBORObject cbornumberdouble = CBORObject.FromObject(edec.ToDouble());
        bool issingle = numberinfo["single"].AsBoolean();
        CBORObject cbornumbersingle = CBORObject.FromObject(edec.ToSingle());
        if (numberinfo["int32"].AsBoolean()) {
          Assert.AreEqual(
    BigInteger.fromString(numberinfo["integer"].AsString()).intValueChecked(),
    cbornumber.AsInt32());
          if (isdouble) {
            Assert.AreEqual(
    BigInteger.fromString(numberinfo["integer"].AsString()).intValueChecked(),
    cbornumberdouble.AsInt32());
          }
          if (issingle) {
            Assert.AreEqual(
    BigInteger.fromString(numberinfo["integer"].AsString()).intValueChecked(),
    cbornumbersingle.AsInt32());
          }
        } else {
          try {
            cbornumber.AsInt32();
            Assert.Fail("Should have failed " + cbornumber);
          } catch (OverflowException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString() + cbornumber);
            throw new InvalidOperationException(String.Empty, ex);
          }
          if (isdouble) {
            try {
              cbornumberdouble.AsInt32();
              Assert.Fail("Should have failed");
            } catch (OverflowException) {
              Console.Write(String.Empty);
            } catch (Exception ex) {
              Assert.Fail(ex.ToString());
              throw new InvalidOperationException(String.Empty, ex);
            }
          }
          if (issingle) {
            try {
              cbornumbersingle.AsInt32();
              Assert.Fail("Should have failed");
            } catch (OverflowException) {
              Console.Write(String.Empty);
            } catch (Exception ex) {
              Assert.Fail(ex.ToString());
              throw new InvalidOperationException(String.Empty, ex);
            }
          }
        }
      }
    }
    [Test]
    public void TestAsInt64() {
      try {
        CBORObject.NewArray().AsInt64();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsInt64();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsInt64();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsInt64();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsInt64();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsInt64();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        ExtendedDecimal edec =
          ExtendedDecimal.FromString(numberinfo["number"].AsString());
        CBORObject cbornumber = CBORObject.FromObject(edec);
        bool isdouble = numberinfo["double"].AsBoolean();
        CBORObject cbornumberdouble = CBORObject.FromObject(edec.ToDouble());
        bool issingle = numberinfo["single"].AsBoolean();
        CBORObject cbornumbersingle = CBORObject.FromObject(edec.ToSingle());
        if (numberinfo["int64"].AsBoolean()) {
          Assert.AreEqual(
   BigInteger.fromString(numberinfo["integer"].AsString()).longValueChecked(),
   cbornumber.AsInt64());
          if (isdouble) {
            Assert.AreEqual(
   BigInteger.fromString(numberinfo["integer"].AsString()).longValueChecked(),
   cbornumberdouble.AsInt64());
          }
          if (issingle) {
            Assert.AreEqual(
   BigInteger.fromString(numberinfo["integer"].AsString()).longValueChecked(),
   cbornumbersingle.AsInt64());
          }
        } else {
          try {
            cbornumber.AsInt64();
            Assert.Fail("Should have failed " + cbornumber);
          } catch (OverflowException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString() + cbornumber);
            throw new InvalidOperationException(String.Empty, ex);
          }
          if (isdouble) {
            try {
              cbornumberdouble.AsInt64();
              Assert.Fail("Should have failed");
            } catch (OverflowException) {
              Console.Write(String.Empty);
            } catch (Exception ex) {
              Assert.Fail(ex.ToString());
              throw new InvalidOperationException(String.Empty, ex);
            }
          }
          if (issingle) {
            try {
              cbornumbersingle.AsInt64();
              Assert.Fail("Should have failed");
            } catch (OverflowException) {
              Console.Write(String.Empty);
            } catch (Exception ex) {
              Assert.Fail(ex.ToString());
              throw new InvalidOperationException(String.Empty, ex);
            }
          }
        }
      }
    }
    [Test]
    public void TestAsSByte() {
      // not implemented yet
    }
    [Test]
    public void TestAsSingle() {
      try {
        CBORObject.NewArray().AsSingle();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().AsSingle();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.AsSingle();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.AsSingle();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Undefined.AsSingle();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(String.Empty).AsSingle();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        AreEqualExact(
(float)ExtendedDecimal.FromString(numberinfo["number"].AsString()).ToSingle(),
cbornumber.AsSingle());
      }
    }
    [Test]
    public void TestAsString() {
      // not implemented yet
    }
    [Test]
    public void TestAsUInt16() {
      // not implemented yet
    }
    [Test]
    public void TestAsUInt32() {
      // not implemented yet
    }
    [Test]
    public void TestAsUInt64() {
      // not implemented yet
    }
    [Test]
    public void TestCanFitInDouble() {
      Assert.IsTrue(CBORObject.FromObject(0).CanFitInDouble());
      Assert.IsFalse(CBORObject.True.CanFitInDouble());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).CanFitInDouble());
      Assert.IsFalse(CBORObject.NewArray().CanFitInDouble());
      Assert.IsFalse(CBORObject.NewMap().CanFitInDouble());
      Assert.IsFalse(CBORObject.False.CanFitInDouble());
      Assert.IsFalse(CBORObject.Null.CanFitInDouble());
      Assert.IsFalse(CBORObject.Undefined.CanFitInDouble());
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (numberinfo["double"].AsBoolean()) {
          Assert.IsTrue(cbornumber.CanFitInDouble());
        } else {
          Assert.IsFalse(cbornumber.CanFitInDouble());
        }
      }
    }
    [Test]
    public void TestCanFitInInt32() {
      Assert.IsTrue(CBORObject.FromObject(0).CanFitInInt32());
      Assert.IsFalse(CBORObject.True.CanFitInInt32());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).CanFitInInt32());
      Assert.IsFalse(CBORObject.NewArray().CanFitInInt32());
      Assert.IsFalse(CBORObject.NewMap().CanFitInInt32());
      Assert.IsFalse(CBORObject.False.CanFitInInt32());
      Assert.IsFalse(CBORObject.Null.CanFitInInt32());
      Assert.IsFalse(CBORObject.Undefined.CanFitInInt32());
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (numberinfo["int32"].AsBoolean() && numberinfo["isintegral"
                     ].AsBoolean()) {
          Assert.IsTrue(cbornumber.CanFitInInt32());
          Assert.IsTrue(CBORObject.FromObject(cbornumber.AsInt32())
                    .CanFitInInt32());
        } else {
          Assert.IsFalse(cbornumber.CanFitInInt32());
        }
      }
    }
    [Test]
    public void TestCanFitInInt64() {
      Assert.IsTrue(CBORObject.FromObject(0).CanFitInSingle());
      Assert.IsFalse(CBORObject.True.CanFitInSingle());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).CanFitInSingle());
      Assert.IsFalse(CBORObject.NewArray().CanFitInSingle());
      Assert.IsFalse(CBORObject.NewMap().CanFitInSingle());
      Assert.IsFalse(CBORObject.False.CanFitInSingle());
      Assert.IsFalse(CBORObject.Null.CanFitInSingle());
      Assert.IsFalse(CBORObject.Undefined.CanFitInSingle());
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (numberinfo["int64"].AsBoolean() && numberinfo["isintegral"
                     ].AsBoolean()) {
          Assert.IsTrue(cbornumber.CanFitInInt64());
          Assert.IsTrue(CBORObject.FromObject(cbornumber.AsInt64())
                    .CanFitInInt64());
        } else {
          Assert.IsFalse(cbornumber.CanFitInInt64());
        }
      }
    }
    [Test]
    public void TestCanFitInSingle() {
      Assert.IsTrue(CBORObject.FromObject(0).CanFitInSingle());
      Assert.IsFalse(CBORObject.True.CanFitInSingle());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).CanFitInSingle());
      Assert.IsFalse(CBORObject.NewArray().CanFitInSingle());
      Assert.IsFalse(CBORObject.NewMap().CanFitInSingle());
      Assert.IsFalse(CBORObject.False.CanFitInSingle());
      Assert.IsFalse(CBORObject.Null.CanFitInSingle());
      Assert.IsFalse(CBORObject.Undefined.CanFitInSingle());
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (numberinfo["single"].AsBoolean()) {
          Assert.IsTrue(cbornumber.CanFitInSingle());
        } else {
          Assert.IsFalse(cbornumber.CanFitInSingle());
        }
      }
    }
    [Test]
    public void TestCanTruncatedIntFitInInt32() {
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        11)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        12)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        13)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        14)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        15)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        16)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        17)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        18)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        19)).CanTruncatedIntFitInInt32());
      Assert.IsTrue(CBORObject.FromObject(0).CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.True.CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.FromObject(String.Empty)
                    .CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.NewArray().CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.NewMap().CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.False.CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.Null.CanTruncatedIntFitInInt32());
      Assert.IsFalse(CBORObject.Undefined.CanTruncatedIntFitInInt32());
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(
            ExtendedDecimal.FromString(numberinfo["number"].AsString()));
        if (numberinfo["int32"].AsBoolean()) {
          Assert.IsTrue(cbornumber.CanTruncatedIntFitInInt32());
        } else {
          Assert.IsFalse(cbornumber.CanTruncatedIntFitInInt32());
        }
      }
    }
    [Test]
    public void TestCanTruncatedIntFitInInt64() {
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        11)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        12)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        13)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        14)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        15)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        16)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        17)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        18)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(ExtendedFloat.Create(
        -2,
        19)).CanTruncatedIntFitInInt64());
      Assert.IsTrue(CBORObject.FromObject(0).CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.True.CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.FromObject(String.Empty)
                    .CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.NewArray().CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.NewMap().CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.False.CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.Null.CanTruncatedIntFitInInt64());
      Assert.IsFalse(CBORObject.Undefined.CanTruncatedIntFitInInt64());
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (numberinfo["int64"].AsBoolean()) {
          Assert.IsTrue(cbornumber.CanTruncatedIntFitInInt64());
        } else {
          Assert.IsFalse(cbornumber.CanTruncatedIntFitInInt64());
        }
      }
    }

    internal static void CompareDecimals(CBORObject o1, CBORObject o2) {
      int cmpDecFrac = TestCommon.CompareTestReciprocal(
        o1.AsExtendedDecimal(),
        o2.AsExtendedDecimal());
      int cmpCobj = TestCommon.CompareTestReciprocal(o1, o2);
      if (cmpDecFrac != cmpCobj) {
        Assert.AreEqual(
          cmpDecFrac,
          cmpCobj,
          TestCommon.ObjectMessages(o1, o2, "Compare: Results don't match"));
      }
      TestCommon.AssertRoundTrip(o1);
      TestCommon.AssertRoundTrip(o2);
    }

    [Test]
    public void TestCompareTo() {
      var r = new FastRandom();
      const int CompareCount = 500;
      for (var i = 0; i < CompareCount; ++i) {
        CBORObject o1 = RandomObjects.RandomCBORObject(r);
        CBORObject o2 = RandomObjects.RandomCBORObject(r);
        CBORObject o3 = RandomObjects.RandomCBORObject(r);
        TestCommon.CompareTestRelations(o1, o2, o3);
      }
      for (var i = 0; i < 5000; ++i) {
        CBORObject o1 = RandomObjects.RandomNumber(r);
        CBORObject o2 = RandomObjects.RandomNumber(r);
        CompareDecimals(o1, o2);
      }
      TestCommon.CompareTestEqual(
CBORObject.FromObject(0.1),
CBORObject.FromObject(0.1));
      TestCommon.CompareTestEqual(
CBORObject.FromObject(0.1f),
CBORObject.FromObject(0.1f));
      for (var i = 0; i < 50; ++i) {
        CBORObject o1 = CBORObject.FromObject(Single.NegativeInfinity);
        CBORObject o2 = RandomObjects.RandomNumberOrRational(r);
        if (o2.IsInfinity() || o2.IsNaN()) {
          continue;
        }
        TestCommon.CompareTestLess(o1, o2);
        o1 = CBORObject.FromObject(Double.NegativeInfinity);
        TestCommon.CompareTestLess(o1, o2);
        o1 = CBORObject.FromObject(Single.PositiveInfinity);
        TestCommon.CompareTestLess(o2, o1);
        o1 = CBORObject.FromObject(Double.PositiveInfinity);
        TestCommon.CompareTestLess(o2, o1);
        o1 = CBORObject.FromObject(Single.NaN);
        TestCommon.CompareTestLess(o2, o1);
        o1 = CBORObject.FromObject(Double.NaN);
        TestCommon.CompareTestLess(o2, o1);
      }
      byte[] bytes1 = { 0, 1 };
      byte[] bytes2 = { 0, 2 };
      byte[] bytes3 = { 0, 2, 0 };
      byte[] bytes4 = { 1, 1 };
      byte[] bytes5 = { 1, 1, 4 };
      byte[] bytes6 = { 1, 2 };
      byte[] bytes7 = { 1, 2, 6 };
      CBORObject[] sortedObjects = {
        CBORObject.Undefined, CBORObject.Null,
        CBORObject.False, CBORObject.True,
        CBORObject.FromObject(Double.NegativeInfinity),
        CBORObject.FromObject(ExtendedDecimal.FromString("-1E+5000")),
        CBORObject.FromObject(Int64.MinValue),
        CBORObject.FromObject(Int32.MinValue),
        CBORObject.FromObject(-2), CBORObject.FromObject(-1),
        CBORObject.FromObject(0), CBORObject.FromObject(1),
        CBORObject.FromObject(2), CBORObject.FromObject(Int64.MaxValue),
        CBORObject.FromObject(ExtendedDecimal.FromString("1E+5000")),
        CBORObject.FromObject(Double.PositiveInfinity),
        CBORObject.FromObject(Double.NaN), CBORObject.FromSimpleValue(0),
        CBORObject.FromSimpleValue(19), CBORObject.FromSimpleValue(32),
        CBORObject.FromSimpleValue(255), CBORObject.FromObject(bytes1),
        CBORObject.FromObject(bytes2), CBORObject.FromObject(bytes3),
        CBORObject.FromObject(bytes4), CBORObject.FromObject(bytes5),
        CBORObject.FromObject(bytes6), CBORObject.FromObject(bytes7),
        CBORObject.FromObject("aa"), CBORObject.FromObject("ab"),
        CBORObject.FromObject("abc"), CBORObject.FromObject("ba"),
        CBORObject.FromObject(CBORObject.NewArray()),
        CBORObject.FromObject(CBORObject.NewMap()),
      };
      for (var i = 0; i < sortedObjects.Length; ++i) {
        for (int j = i; j < sortedObjects.Length; ++j) {
          if (i == j) {
            TestCommon.CompareTestEqual(sortedObjects[i], sortedObjects[j]);
          } else {
            TestCommon.CompareTestLess(sortedObjects[i], sortedObjects[j]);
          }
        }
        Assert.AreEqual(1, sortedObjects[i].CompareTo(null));
      }
      CBORObject sp = CBORObject.FromObject(Single.PositiveInfinity);
      CBORObject sn = CBORObject.FromObject(Single.NegativeInfinity);
      CBORObject snan = CBORObject.FromObject(Single.NaN);
      CBORObject dp = CBORObject.FromObject(Double.PositiveInfinity);
      CBORObject dn = CBORObject.FromObject(Double.NegativeInfinity);
      CBORObject dnan = CBORObject.FromObject(Double.NaN);
      TestCommon.CompareTestEqual(sp, sp);
      TestCommon.CompareTestEqual(sp, dp);
      TestCommon.CompareTestEqual(dp, dp);
      TestCommon.CompareTestEqual(sn, sn);
      TestCommon.CompareTestEqual(sn, dn);
      TestCommon.CompareTestEqual(dn, dn);
      TestCommon.CompareTestEqual(snan, snan);
      TestCommon.CompareTestEqual(snan, dnan);
      TestCommon.CompareTestEqual(dnan, dnan);
      TestCommon.CompareTestLess(sn, sp);
      TestCommon.CompareTestLess(sn, dp);
      TestCommon.CompareTestLess(sn, snan);
      TestCommon.CompareTestLess(sn, dnan);
      TestCommon.CompareTestLess(sp, snan);
      TestCommon.CompareTestLess(sp, dnan);
      TestCommon.CompareTestLess(dn, dp);
      TestCommon.CompareTestLess(dp, dnan);
      Assert.AreEqual(1, CBORObject.True.CompareTo(null));
      Assert.AreEqual(1, CBORObject.False.CompareTo(null));
      Assert.AreEqual(1, CBORObject.Null.CompareTo(null));
      Assert.AreEqual(1, CBORObject.NewArray().CompareTo(null));
      Assert.AreEqual(1, CBORObject.NewMap().CompareTo(null));
      Assert.AreEqual(1, CBORObject.FromObject(100).CompareTo(null));
      Assert.AreEqual(1, CBORObject.FromObject(Double.NaN).CompareTo(null));
      TestCommon.CompareTestLess(CBORObject.Undefined, CBORObject.Null);
      TestCommon.CompareTestLess(CBORObject.Null, CBORObject.False);
      TestCommon.CompareTestLess(CBORObject.False, CBORObject.True);
      TestCommon.CompareTestLess(CBORObject.False, CBORObject.FromObject(0));
      TestCommon.CompareTestLess(
   CBORObject.False,
   CBORObject.FromSimpleValue(0));
      TestCommon.CompareTestLess(
        CBORObject.FromSimpleValue(0),
        CBORObject.FromSimpleValue(1));
      TestCommon.CompareTestLess(
        CBORObject.FromObject(0),
        CBORObject.FromObject(1));
      TestCommon.CompareTestLess(
        CBORObject.FromObject(0.0f),
        CBORObject.FromObject(1.0f));
      TestCommon.CompareTestLess(
        CBORObject.FromObject(0.0),
        CBORObject.FromObject(1.0));
    }
    [Test]
    public void TestContainsKey() {
      // not implemented yet
    }
    [Test]
    public void TestCount() {
      Assert.AreEqual(0, CBORObject.True.Count);
      Assert.AreEqual(0, CBORObject.False.Count);
      Assert.AreEqual(0, CBORObject.NewArray().Count);
      Assert.AreEqual(0, CBORObject.NewMap().Count);
    }

    [Test]
    public void TestDecodeFromBytesNoDuplicateKeys() {
      byte[] bytes;
      bytes = new byte[] { 0xa2, 0x01, 0x00, 0x02, 0x03 };
      try {
        CBORObject.DecodeFromBytes(bytes, CBOREncodeOptions.NoDuplicateKeys);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      bytes = new byte[] { 0xa2, 0x01, 0x00, 0x01, 0x03 };
      try {
        CBORObject.DecodeFromBytes(bytes, CBOREncodeOptions.NoDuplicateKeys);
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      bytes = new byte[] { 0xa2, 0x01, 0x00, 0x01, 0x03 };
      try {
        CBORObject.DecodeFromBytes(bytes, CBOREncodeOptions.None);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      bytes = new byte[] { 0xa2, 0x60, 0x00, 0x60, 0x03 };
      try {
        CBORObject.DecodeFromBytes(bytes, CBOREncodeOptions.NoDuplicateKeys);
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
   bytes = new byte[] { 0xa3, 0x60, 0x00, 0x62, 0x41, 0x41, 0x00, 0x60, 0x03 };
      try {
        CBORObject.DecodeFromBytes(bytes, CBOREncodeOptions.NoDuplicateKeys);
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      bytes = new byte[] { 0xa2, 0x61, 0x41, 0x00, 0x61, 0x41, 0x03 };
      try {
        CBORObject.DecodeFromBytes(bytes, CBOREncodeOptions.NoDuplicateKeys);
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestDecodeFromBytes() {
      try {
        CBORObject.DecodeFromBytes(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.DecodeFromBytes(new byte[] { });
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
 CBORObject.DecodeFromBytes(new byte[] { 0 }, null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      try {
        CBORObject.DecodeFromBytes(new byte[] { 0x1c });
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.DecodeFromBytes(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.DecodeFromBytes(new byte[] { 0x1e });
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.DecodeFromBytes(new byte[] { 0xfe });
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.DecodeFromBytes(new byte[] { 0xff });
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestDivide() {
      try {
        CBORObject.Divide(null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Divide(CBORObject.FromObject(2), null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Divide(CBORObject.Null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Divide(CBORObject.FromObject(2), CBORObject.Null);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestEncodeToBytes() {
      // Test minimum data length
      int[] ranges = {
        -24, 23, 1,
        -256, -25, 2,
        24, 255, 2,
        256, 266, 3,
        -266, -257, 3,
        65525, 65535, 3,
        -65536, -65525, 3,
        65536, 65546, 5,
        -65547, -65537, 5,
      };
      string[] bigRanges = {
        "4294967285", "4294967295",
        "4294967296", "4294967306",
        "18446744073709551604", "18446744073709551615",
        "-4294967296", "-4294967286",
        "-4294967306", "-4294967297",
        "-18446744073709551616", "-18446744073709551604"
      };
      int[] bigSizes = { 5, 9, 9, 5, 9, 9 };
      for (int i = 0; i < ranges.Length; i += 3) {
        for (int j = ranges[i]; j <= ranges[i + 1]; ++j) {
          byte[] bytes = CBORObject.FromObject(j).EncodeToBytes();
          if (bytes.Length != ranges[i + 2]) {
            Assert.AreEqual(
ranges[i + 2],
bytes.Length,
TestCommon.IntToString(j));
          }
        }
      }
      for (int i = 0; i < bigRanges.Length; i += 2) {
        BigInteger bj = BigInteger.fromString(bigRanges[i]);
        BigInteger valueBjEnd = BigInteger.fromString(bigRanges[i + 1]);
        while (bj < valueBjEnd) {
          byte[] bytes = CBORObject.FromObject(bj).EncodeToBytes();
          if (bytes.Length != bigSizes[i / 2]) {
            Assert.AreEqual(bigSizes[i / 2], bytes.Length, bj.ToString());
          }
          bj += BigInteger.One;
        }
      }
      try {
 CBORObject.True.EncodeToBytes(null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
    }
    [Test]
    public void TestEquals() {
      // not implemented yet
    }

    public string CharString(int cp, bool quoted, char[] charbuf) {
      var index = 0;
      if (quoted) {
        charbuf[index++] = (char)0x22;
      }
      if (cp < 0x10000) {
        if (cp >= 0xd800 && cp < 0xe000) {
          return null;
        }
        charbuf[index++] = (char)cp;
        if (quoted) {
          charbuf[index++] = (char)0x22;
        }
        return new String(charbuf, 0, index);
      } else {
        cp -= 0x10000;
        charbuf[index++] = (char)((cp / 0x400) + 0xd800);
        charbuf[index++] = (char)((cp & 0x3ff) + 0xdc00);
        if (quoted) {
          charbuf[index++] = (char)0x22;
        }
        return new String(charbuf, 0, index);
      }
    }

    public static void TestFailingJSON(string str) {
      TestFailingJSON(str, CBOREncodeOptions.None);
    }

    public static void TestFailingJSON(string str, CBOREncodeOptions opt) {
      byte[] bytes = null;
      try {
        bytes = DataUtilities.GetUtf8Bytes(str, false);
      } catch (ArgumentException ex2) {
        // Check only FromJSONString
        try {
          if (opt.Value == 0) {
            CBORObject.FromJSONString(str);
          } else {
            CBORObject.FromJSONString(str, opt);
          }
          Assert.Fail("Should have failed");
        } catch (CBORException) {
          Console.Write(String.Empty);
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        return;
      }
      using (var ms = new MemoryStream(bytes)) {
        try {
          if (opt.Value == 0) {
            CBORObject.ReadJSON(ms);
          } else {
            CBORObject.ReadJSON(ms, opt);
          }
          Assert.Fail("Should have failed");
        } catch (CBORException) {
          Console.Write(String.Empty);
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      }
      try {
        if (opt.Value == 0) {
          CBORObject.FromJSONString(str);
        } else {
          CBORObject.FromJSONString(str, opt);
        }
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    public static CBORObject TestSucceedingJSON(string str) {
      byte[] bytes = DataUtilities.GetUtf8Bytes(str, false);
      try {
      using (var ms = new MemoryStream(bytes)) {
        CBORObject obj = CBORObject.ReadJSON(ms);
        TestCommon.CompareTestEqualAndConsistent(
          obj,
          CBORObject.FromJSONString(str));
        TestCommon.AssertRoundTrip(obj);
        return obj;
      }
    } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
    }
    }

    private static string[] jsonFails = {"\"\\uxxxx\"" , "\"\\ud800\udc00\"",
      "\"\ud800\\udc00\"" , "\"\\U0023\"" , "\"\\u002x\"" , "\"\\u00xx\"",
      "\"\\u0xxx\"" , "\"\\u0\"" , "\"\\u00\"" , "\"\\u000\"" , "trbb",
      "trub" , "falsb" , "nulb" , "[true" , "[true," , "[true]!",
      "[\"\ud800\\udc00\"]" , "[\"\\ud800\udc00\"]",
      "[\"\\udc00\ud800\udc00\"]" , "[\"\\ud800\ud800\udc00\"]",
      "[\"\\ud800\"]" , "[1,2," , "[1,2,3" , "{,\"0\":0,\"1\":1}",
      "{\"0\":0,,\"1\":1}" , "{\"0\":0,\"1\":1,}" , "[,0,1,2]" , "[0,,1,2]",
"[0,1,,2]" , "[0,1,2,]" , "[0001]" , "{a:true}",
      "{\"a\"://comment\ntrue}" , "{\"a\":/*comment*/true}" , "{'a':true}",
      "{\"a\":'b'}" , "{\"a\t\":true}" , "{\"a\r\":true}" , "{\"a\n\":true}",
"['a']" , "{\"a\":\"a\t\"}" , "[\"a\\'\"]" , "[NaN]" , "[+Infinity]",
"[-Infinity]" , "[Infinity]" , "{\"a\":\"a\r\"}" , "{\"a\":\"a\n\"}",
"[\"a\t\"]" , "\"test\"\"" , "\"test\"x" , "\"test\"\u0300",
      "\"test\"\u0005" , "[5]\"" , "[5]x" , "[5]\u0300" , "[5]\u0005",
      "{\"test\":5}\"" , "{\"test\":5}x" , "{\"test\":5}\u0300",
      "{\"test\":5}\u0005" , "true\"" , "truex" , "true}" , "true\u0300",
      "true\u0005" , "8024\"" , "8024x" , "8024}" , "8024\u0300",
      "8024\u0005" , "{\"test\":5}}" , "{\"test\":5}{" , "[5]]" , "[5][",
      "0000" , "0x1" , "0xf" , "0x20" , "0x01",
      "0X1" , "0Xf" , "0X20" , "0X01" , ".2" , ".05" , "-.2",
      "-.05" , "23." , "23.e0" , "23.e1" , "0." , "[0000]" , "[0x1]",
      "[0xf]" , "[0x20]" , "[0x01]" , "[.2]" , "[.05]" , "[-.2]" , "[-.05]",
"[23.]" , "[23.e0]" , "[23.e1]" , "[0.]" , "\"abc" , "\"ab\u0004c\"",
"\u0004\"abc\"" , "[1,\u0004" + "2]" };

 private static string[] jsonSucceeds ={"[0]" , "[0.1]" , "[0.1001]",
      "[0.0]",
"[0.00]" , "[0.000]" , "[0.01]" , "[0.001]" , "[0.5]" , "[0E5]",
  "[0E+6]" , "[\"\ud800\udc00\"]" , "[\"\\ud800\\udc00\"]",
  "[\"\\ud800\\udc00\ud800\udc00\"]" , "23.0e01" , "23.0e00" , "[23.0e01]",
  "[23.0e00]" , "0" ,"1" ,"0.2" ,"0.05" ,"-0.2" ,"-0.05" };

    [Test]
    public void TestFromJSONString() {
      var charbuf = new char[4];
      CBORObject cbor;
      // Test single-character strings
      for (var i = 0; i < 0x110000; ++i) {
        if (i >= 0xd800 && i < 0xe000) {
          continue;
        }
        string str = this.CharString(i, true, charbuf);
        if (i < 0x20 || i == 0x22 || i == 0x5c) {
          TestFailingJSON(str);
        } else {
          cbor = TestSucceedingJSON(str);
          string exp = this.CharString(i, false, charbuf);
          if (!exp.Equals(cbor.AsString())) {
            Assert.AreEqual(exp, cbor.AsString());
          }
        }
      }
      foreach (string str in jsonFails) {
        TestFailingJSON(str);
      }
      foreach (string str in jsonSucceeds) {
        TestSucceedingJSON(str);
      }
      try {
        CBORObject.FromJSONString("\ufeff\u0020 {}");
        Assert.Fail("Should have failed");
      } catch (CBORException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
 CBORObject.FromJSONString("[]", null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      TestFailingJSON("{\"a\":1,\"a\":2}", CBOREncodeOptions.NoDuplicateKeys);
      string aba = "{\"a\":1,\"b\":3,\"a\":2}";
      TestFailingJSON(aba, CBOREncodeOptions.NoDuplicateKeys);
      cbor = TestSucceedingJSON(aba);
      Assert.AreEqual(CBORObject.FromObject(2), cbor["a"]);
      cbor = TestSucceedingJSON("{\"a\":1,\"a\":4}");
      Assert.AreEqual(CBORObject.FromObject(4), cbor["a"]);
      Assert.AreEqual(CBORObject.True, TestSucceedingJSON("true"));
      Assert.AreEqual(CBORObject.False, TestSucceedingJSON("false"));
      Assert.AreEqual(CBORObject.Null, TestSucceedingJSON("null"));
      Assert.AreEqual(5, TestSucceedingJSON(" 5 ").AsInt32());
      {
        string stringTemp = TestSucceedingJSON("\"\\/\\b\"").AsString();
        Assert.AreEqual(
        "/\b",
        stringTemp);
      }
      {
        string stringTemp = TestSucceedingJSON("\"\\/\\f\"").AsString();
        Assert.AreEqual(
        "/\f",
        stringTemp);
      }
      string jsonTemp = TestCommon.Repeat(
     "[",
     2000) + TestCommon.Repeat(
     "]",
     2000);
      TestFailingJSON(jsonTemp);
    }
    [Test]
    public void TestFromObject() {
      var cborarray = new CBORObject[2];
      cborarray[0] = CBORObject.False;
      cborarray[1] = CBORObject.True;
      CBORObject cbor = CBORObject.FromObject(cborarray);
      Assert.AreEqual(2, cbor.Count);
      Assert.AreEqual(CBORObject.False, cbor[0]);
      Assert.AreEqual(CBORObject.True, cbor[1]);
      TestCommon.AssertRoundTrip(cbor);
      Assert.AreEqual(CBORObject.Null, CBORObject.FromObject((int[])null));
      long[] longarray = { 2, 3 };
      cbor = CBORObject.FromObject(longarray);
      Assert.AreEqual(2, cbor.Count);
      Assert.IsTrue(CBORObject.FromObject(2).CompareTo(cbor[0]) == 0);
      Assert.IsTrue(CBORObject.FromObject(3).CompareTo(cbor[1]) == 0);
      TestCommon.AssertRoundTrip(cbor);
      Assert.AreEqual(
        CBORObject.Null,
        CBORObject.FromObject((ExtendedRational)null));
      Assert.AreEqual(
        CBORObject.Null,
        CBORObject.FromObject((ExtendedDecimal)null));
      Assert.AreEqual(
        CBORObject.FromObject(10),
        CBORObject.FromObject(ExtendedRational.Create(10, 1)));
      try {
        CBORObject.FromObject(ExtendedRational.Create(10, 2));
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        CBORObject.FromObject(CBORObject.FromObject(Double.NaN).Sign);
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      cbor = CBORObject.True;
      try {
        CBORObject.FromObject(cbor[0]);
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        cbor[0] = CBORObject.False;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        cbor = CBORObject.False;
        CBORObject.FromObject(cbor.Keys);
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(CBORObject.NewArray().Keys);
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(CBORObject.NewArray().Sign);
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(CBORObject.NewMap().Sign);
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestFromObjectAndTag() {
      BigInteger bigvalue = BigInteger.One << 100;
      try {
        CBORObject.FromObjectAndTag(2, bigvalue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObjectAndTag(2, -1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObjectAndTag(CBORObject.Null, -1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObjectAndTag(CBORObject.Null, 999999);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObjectAndTag(2, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObjectAndTag(2, BigInteger.Zero - BigInteger.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestFromSimpleValue() {
      try {
        CBORObject.FromSimpleValue(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromSimpleValue(256);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      for (int i = 0; i < 256; ++i) {
        if (i >= 24 && i < 32) {
          try {
            CBORObject.FromSimpleValue(i);
            Assert.Fail("Should have failed");
          } catch (ArgumentException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        } else {
          CBORObject cbor = CBORObject.FromSimpleValue(i);
          Assert.AreEqual(i, cbor.SimpleValue);
        }
      }
    }
    [Test]
    public void TestGetByteString() {
      try {
        CBORObject.True.GetByteString();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject(0).GetByteString();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.FromObject("test").GetByteString();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.GetByteString();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewArray().GetByteString();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().GetByteString();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestGetHashCode() {
      // not implemented yet
    }
    [Test]
    public void TestGetTags() {
      // not implemented yet
    }
    [Test]
    public void TestHasTag() {
      try {
        CBORObject.True.HasTag(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        const BigInteger ValueBigintNull = null;
        CBORObject.True.HasTag(ValueBigintNull);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.True.HasTag(BigInteger.One.negate());
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.IsFalse(CBORObject.True.HasTag(0));
      Assert.IsFalse(CBORObject.True.HasTag(BigInteger.Zero));
    }
    [Test]
    public void TestInnermostTag() {
      // not implemented yet
    }
    [Test]
    public void TestInsert() {
      // not implemented yet
    }
    [Test]
    public void TestIsFalse() {
      // not implemented yet
    }
    [Test]
    public void TestIsFinite() {
      CBORObject cbor;
      Assert.IsTrue(CBORObject.FromObject(0).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsFinite);
      Assert.IsFalse(CBORObject.NewArray().IsFinite);
      Assert.IsFalse(CBORObject.NewMap().IsFinite);
      cbor = CBORObject.True;
      Assert.IsFalse(cbor.IsFinite);
      cbor = CBORObject.False;
      Assert.IsFalse(cbor.IsFinite);
      cbor = CBORObject.Null;
      Assert.IsFalse(cbor.IsFinite);
      cbor = CBORObject.Undefined;
      Assert.IsFalse(cbor.IsFinite);
      Assert.IsFalse(CBORObject.NewMap().IsFinite);
      Assert.IsTrue(CBORObject.FromObject(0).IsFinite);
      Assert.IsTrue(CBORObject.FromObject(2.5).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(Double.PositiveInfinity).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(Double.NegativeInfinity).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(Double.NaN).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(
        ExtendedDecimal.PositiveInfinity).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(
        ExtendedDecimal.NegativeInfinity).IsFinite);
      Assert.IsFalse(CBORObject.FromObject(ExtendedDecimal.NaN).IsFinite);
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (!numberinfo["integer"].Equals(CBORObject.Null)) {
          Assert.IsTrue(cbornumber.IsFinite);
        } else {
          Assert.IsFalse(cbornumber.IsFinite);
        }
      }
    }
    [Test]
    public void TestIsInfinity() {
      Assert.IsTrue(CBORObject.DecodeFromBytes(new byte[] { (byte)0xfa, 0x7f,
        (byte)0x80, 0x00, 0x00 }).IsInfinity());
    }

    public static CBORObject GetNumberData() {
      return new AppResources("Resources").GetJSON("numbers");
    }

    [Test]
    public void TestIsIntegral() {
      CBORObject cbor;
      Assert.IsTrue(CBORObject.FromObject(0).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsIntegral);
      Assert.IsFalse(CBORObject.NewArray().IsIntegral);
      Assert.IsFalse(CBORObject.NewMap().IsIntegral);
      Assert.IsTrue(CBORObject.FromObject(BigInteger.One << 63).IsIntegral);
      Assert.IsTrue(CBORObject.FromObject(BigInteger.One << 64).IsIntegral);
      Assert.IsTrue(CBORObject.FromObject(BigInteger.One << 80).IsIntegral);
      Assert.IsTrue(CBORObject.FromObject(
        ExtendedDecimal.FromString("4444e+800")).IsIntegral);

      Assert.IsFalse(CBORObject.FromObject(
        ExtendedDecimal.FromString("4444e-800")).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(2.5).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(999.99).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(Double.PositiveInfinity).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(Double.NegativeInfinity).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(Double.NaN).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(
        ExtendedDecimal.PositiveInfinity).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(
        ExtendedDecimal.NegativeInfinity).IsIntegral);
      Assert.IsFalse(CBORObject.FromObject(ExtendedDecimal.NaN).IsIntegral);
      cbor = CBORObject.True;
      Assert.IsFalse(cbor.IsIntegral);
      cbor = CBORObject.False;
      Assert.IsFalse(cbor.IsIntegral);
      cbor = CBORObject.Null;
      Assert.IsFalse(cbor.IsIntegral);
      cbor = CBORObject.Undefined;
      Assert.IsFalse(cbor.IsIntegral);
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(
            numberinfo["number"].AsString()));
        if (numberinfo["isintegral"].AsBoolean()) {
          Assert.IsTrue(cbornumber.IsIntegral);
          Assert.IsFalse(cbornumber.IsPositiveInfinity());
          Assert.IsFalse(cbornumber.IsNegativeInfinity());
          Assert.IsFalse(cbornumber.IsNaN());
          Assert.IsFalse(cbornumber.IsNull);
        } else {
          Assert.IsFalse(cbornumber.IsIntegral);
        }
      }
    }
    [Test]
    public void TestIsNaN() {
      Assert.IsFalse(CBORObject.True.IsNaN());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsNaN());
      Assert.IsFalse(CBORObject.NewArray().IsNaN());
      Assert.IsFalse(CBORObject.NewMap().IsNaN());
      Assert.IsFalse(CBORObject.False.IsNaN());
      Assert.IsFalse(CBORObject.Null.IsNaN());
      Assert.IsFalse(CBORObject.Undefined.IsNaN());
      Assert.IsFalse(CBORObject.PositiveInfinity.IsNaN());
      Assert.IsFalse(CBORObject.NegativeInfinity.IsNaN());
      Assert.IsTrue(CBORObject.NaN.IsNaN());
    }
    [Test]
    public void TestIsNegativeInfinity() {
      Assert.IsFalse(CBORObject.True.IsNegativeInfinity());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsNegativeInfinity());
      Assert.IsFalse(CBORObject.NewArray().IsNegativeInfinity());
      Assert.IsFalse(CBORObject.NewMap().IsNegativeInfinity());
      Assert.IsFalse(CBORObject.False.IsNegativeInfinity());
      Assert.IsFalse(CBORObject.Null.IsNegativeInfinity());
      Assert.IsFalse(CBORObject.Undefined.IsNegativeInfinity());
      Assert.IsFalse(CBORObject.PositiveInfinity.IsNegativeInfinity());
      Assert.IsTrue(CBORObject.NegativeInfinity.IsNegativeInfinity());
      Assert.IsFalse(CBORObject.NaN.IsNegativeInfinity());
    }
    [Test]
    public void TestIsNull() {
      Assert.IsFalse(CBORObject.True.IsNull);
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsNull);
      Assert.IsFalse(CBORObject.NewArray().IsNull);
      Assert.IsFalse(CBORObject.NewMap().IsNull);
      Assert.IsFalse(CBORObject.False.IsNull);
      Assert.IsTrue(CBORObject.Null.IsNull);
      Assert.IsFalse(CBORObject.Undefined.IsNull);
      Assert.IsFalse(CBORObject.PositiveInfinity.IsNull);
      Assert.IsFalse(CBORObject.NegativeInfinity.IsNull);
      Assert.IsFalse(CBORObject.NaN.IsNull);
    }
    [Test]
    public void TestIsPositiveInfinity() {
      Assert.IsFalse(CBORObject.True.IsPositiveInfinity());
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsPositiveInfinity());
      Assert.IsFalse(CBORObject.NewArray().IsPositiveInfinity());
      Assert.IsFalse(CBORObject.NewMap().IsPositiveInfinity());
      Assert.IsFalse(CBORObject.False.IsPositiveInfinity());
      Assert.IsFalse(CBORObject.Null.IsPositiveInfinity());
      Assert.IsFalse(CBORObject.Undefined.IsPositiveInfinity());
      Assert.IsTrue(CBORObject.PositiveInfinity.IsPositiveInfinity());
      Assert.IsFalse(CBORObject.NegativeInfinity.IsPositiveInfinity());
      Assert.IsFalse(CBORObject.NaN.IsPositiveInfinity());
    }
    [Test]
    public void TestIsTagged() {
      // not implemented yet
    }
    [Test]
    public void TestIsTrue() {
      // not implemented yet
    }
    [Test]
    public void TestIsUndefined() {
      Assert.IsFalse(CBORObject.True.IsUndefined);
      Assert.IsFalse(CBORObject.FromObject(String.Empty).IsUndefined);
      Assert.IsFalse(CBORObject.NewArray().IsUndefined);
      Assert.IsFalse(CBORObject.NewMap().IsUndefined);
      Assert.IsFalse(CBORObject.False.IsUndefined);
      Assert.IsFalse(CBORObject.Null.IsUndefined);
      Assert.IsTrue(CBORObject.Undefined.IsUndefined);
      Assert.IsFalse(CBORObject.PositiveInfinity.IsUndefined);
      Assert.IsFalse(CBORObject.NegativeInfinity.IsUndefined);
      Assert.IsFalse(CBORObject.NaN.IsUndefined);
    }
    [Test]
    public void TestIsZero() {
      // not implemented yet
    }
    [Test]
    public void TestItem() {
      CBORObject cbor = CBORObject.True;
      try {
        CBORObject cbor2 = cbor[0];
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      cbor = CBORObject.False;
      try {
        CBORObject cbor2 = cbor[0];
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      cbor = CBORObject.FromObject(0);
      try {
        CBORObject cbor2 = cbor[0];
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      cbor = CBORObject.FromObject(2);
      try {
        CBORObject cbor2 = cbor[0];
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      cbor = CBORObject.NewArray();
      try {
        CBORObject cbor2 = cbor[0];
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestKeys() {
      // not implemented yet
    }
    [Test]
    public void TestMultiply() {
      try {
        CBORObject.Multiply(null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Multiply(CBORObject.FromObject(2), null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Multiply(CBORObject.Null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Multiply(CBORObject.FromObject(2), CBORObject.Null);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      var r = new FastRandom();
      for (var i = 0; i < 3000; ++i) {
        CBORObject o1 = RandomObjects.RandomNumber(r);
        CBORObject o2 = RandomObjects.RandomNumber(r);
        ExtendedDecimal cmpDecFrac =
          o1.AsExtendedDecimal().Multiply(o2.AsExtendedDecimal());
        ExtendedDecimal cmpCobj = CBORObject.Multiply(
          o1,
          o2).AsExtendedDecimal();
        if (cmpDecFrac.CompareTo(cmpCobj) != 0) {
          Assert.AreEqual(
            0,
            cmpDecFrac.CompareTo(cmpCobj),
            TestCommon.ObjectMessages(o1, o2, "Results don't match"));
        }
        TestCommon.AssertRoundTrip(o1);
        TestCommon.AssertRoundTrip(o2);
      }
    }
    [Test]
    public void TestNegate() {
      Assert.AreEqual(
        CBORObject.FromObject(2),
        CBORObject.FromObject(-2).Negate());
      Assert.AreEqual(
        CBORObject.FromObject(-2),
        CBORObject.FromObject(2).Negate());
      try {
        CBORObject.True.Negate();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.False.Negate();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewArray().Negate();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.NewMap().Negate();
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestNewArray() {
      // not implemented yet
    }
    [Test]
    public void TestNewMap() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorAddition() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorDivision() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorModulus() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorMultiply() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorSubtraction() {
      // not implemented yet
    }
    [Test]
    public void TestOutermostTag() {
      CBORObject cbor = CBORObject.FromObjectAndTag(CBORObject.True, 999);
      cbor = CBORObject.FromObjectAndTag(CBORObject.True, 1000);
      Assert.AreEqual((BigInteger)1000, cbor.OutermostTag);
      cbor = CBORObject.True;
      Assert.AreEqual((BigInteger)(-1), cbor.OutermostTag);
    }
    [Test]
    public void TestRead() {
      try {
        CBORObject.Read(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        using (var ms2 = new MemoryStream(new byte[] { 0 })) {
          try {
 CBORObject.Read(ms2, null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
        }
    } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
    }
    }
    [Test]
    public void TestReadJSON() {
      try {
        using (var ms2 = new MemoryStream(new byte[] { 0x30 })) {
          try {
 CBORObject.ReadJSON(ms2, null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
        }
        using (var ms = new MemoryStream(new byte[] { 0xef, 0xbb, 0xbf, 0x7b,
        0x7d })) {
          try {
            CBORObject.ReadJSON(ms);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        // whitespace followed by BOM
        using (var ms2 = new MemoryStream(new byte[] { 0x20, 0xef, 0xbb, 0xbf,
        0x7b, 0x7d })) {
          try {
            CBORObject.ReadJSON(ms2);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var ms2a = new MemoryStream(new byte[] { 0x7b, 0x05, 0x7d })) {
          try {
            CBORObject.ReadJSON(ms2a);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var ms2b = new MemoryStream(new byte[] { 0x05, 0x7b, 0x7d })) {
          try {
            CBORObject.ReadJSON(ms2b);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        // two BOMs
        using (var ms3 = new MemoryStream(new byte[] { 0xef, 0xbb, 0xbf, 0xef,
        0xbb, 0xbf, 0x7b, 0x7d })) {
          try {
            CBORObject.ReadJSON(ms3);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
          0,
        0,
                    0x74, 0, 0, 0, 0x72, 0, 0, 0, 0x75, 0, 0, 0,
                    0x65 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x74, 0, 0,
        0, 0x72, 0,
                    0, 0, 0x75, 0, 0, 0, 0x65 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0, 0,
        0x74, 0, 0, 0,
                    0x72, 0, 0, 0, 0x75, 0, 0, 0, 0x65, 0, 0, 0 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
      using (var msjson = new MemoryStream(new byte[] { 0x74, 0, 0, 0, 0x72,
          0,
          0,
        0,
                    0x75, 0, 0, 0, 0x65, 0, 0, 0 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0, 0x74,
        0, 0x72, 0,
                    0x75, 0, 0x65 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0x74, 0, 0x72, 0,
        0x75, 0, 0x65 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x74, 0,
          0x72,
        0,
                    0x75,
                    0, 0x65, 0 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0x74, 0, 0x72, 0,
        0x75, 0, 0x65, 0 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0xef, 0xbb, 0xbf,
        0x74, 0x72, 0x75,
       0x65 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
 using (var msjson = new MemoryStream(new byte[] { 0x74, 0x72, 0x75, 0x65 })) {
          Assert.AreEqual(CBORObject.True, CBORObject.ReadJSON(msjson));
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0, 0, 0x22,
                    0, 1, 0, 0, 0, 0, 0, 0x22 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x22, 0, 1,
        0, 0, 0, 0,
                    0, 0x22 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0, 0,
        0x22, 0, 0, 0,
                    0, 0, 1, 0, 0x22, 0, 0, 0 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0x22, 0, 0, 0, 0, 0,
        1, 0, 0x22,
                    0,
                    0, 0 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
   using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0, 0x22, 0xd8,
        0,
                    0xdc, 0, 0, 0x22 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0x22, 0xd8, 0,
        0xdc, 0, 0, 0x22 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x22, 0,
        0, 0xd8, 0,
                    0xdc, 0x22, 0 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0x22, 0, 0, 0xd8, 0,
        0xdc, 0x22, 0 })) {
          {
            string stringTemp = CBORObject.ReadJSON(msjson).AsString();
            Assert.AreEqual(
            "\ud800\udc00",
            stringTemp);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0, 0, 0x22,
                    0, 0, 0xd8, 0, 0, 0, 0, 0x22 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x22, 0, 0,
        0xd8, 0, 0,
                    0,
                    0, 0x22 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0, 0,
        0x22, 0, 0, 0,
                    0, 0xd8, 0, 0, 0x22, 0, 0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      using (var msjson = new MemoryStream(new byte[] { 0x22, 0, 0, 0, 0,
          0xd8,
          0,
        0,
                    0x22, 0, 0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0, 0x22,
        0, 0xdc, 0,
                    0xdc, 0, 0, 0x22 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0x22, 0, 0xdc, 0,
        0xdc, 0, 0,
                    0x22 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x22, 0,
        0, 0xdc, 0,
                    0xdc, 0x22, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0x22, 0, 0, 0xdc, 0,
        0xdc, 0x22, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfc })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        // Illegal UTF-16
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0x20,
        0x20, 0x20 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x20,
        0x20, 0x20 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
 using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xd8, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
 using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xdc, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xd8,
        0x00, 0x20, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xdc,
        0x00, 0x20, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xd8,
        0x00, 0xd8, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xdc,
        0x00, 0xd8, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xdc,
        0x00, 0xd8, 0x00, 0xdc, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xfe, 0xff, 0xdc,
        0x00, 0xdc, 0x00 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }

 using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00, 0xd8 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
 using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00, 0xdc })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00,
        0xd8, 0x00, 0x20 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00,
        0xdc, 0x00, 0x20 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00,
        0xd8, 0x00, 0xd8 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00,
        0xdc, 0x00, 0xd8 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00,
        0xdc, 0x00, 0xd8, 0x00, 0xdc })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0xff, 0xfe, 0x00,
        0xdc, 0x00, 0xdc })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }

        // Illegal UTF-32
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
    using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
 using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0, 0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0, 0,
        0xd8, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0, 0,
        0xdc, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0,
        0x11, 0x00, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0,
        0xff, 0x00, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0, 0x20, 0x1,
        0, 0x00, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
    using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
 using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0, 0xd8, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0, 0xdc, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0x11, 0x00, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff, 0,
        0xff, 0x00, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
        using (var msjson = new MemoryStream(new byte[] { 0, 0, 0xfe, 0xff,
        0x1, 0, 0x00, 0 })) {
          try {
            CBORObject.ReadJSON(msjson);
            Assert.Fail("Should have failed");
          } catch (CBORException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      } catch (IOException ex) {
        Assert.Fail(ex.Message);
      }
    }

    [Test]
    public void TestRemainder() {
      try {
        CBORObject.Remainder(null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Remainder(CBORObject.FromObject(2), null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Remainder(CBORObject.Null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Remainder(CBORObject.FromObject(2), CBORObject.Null);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestRemove() {
      // not implemented yet
    }
    [Test]
    public void TestSet() {
      CBORObject cbor = CBORObject.NewMap().Add("x", 0).Add("y", 1);
      Assert.AreEqual(0, cbor["x"].AsInt32());
      Assert.AreEqual(1, cbor["y"].AsInt32());
      cbor.Set("x", 5).Set("z", 6);
      Assert.AreEqual(5, cbor["x"].AsInt32());
      Assert.AreEqual(6, cbor["z"].AsInt32());
    }
    [Test]
    public void TestSign() {
      try {
        int sign = CBORObject.True.Sign;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        int sign = CBORObject.False.Sign;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        int sign = CBORObject.NewArray().Sign;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        int sign = CBORObject.NewMap().Sign;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      CBORObject numbers = GetNumberData();
      for (int i = 0; i < numbers.Count; ++i) {
        CBORObject numberinfo = numbers[i];
        CBORObject cbornumber =
          CBORObject.FromObject(ExtendedDecimal.FromString(numberinfo["number"
                     ].AsString()));
        if (cbornumber.IsNaN()) {
          try {
            Assert.Fail(String.Empty + cbornumber.Sign);
            Assert.Fail("Should have failed");
          } catch (InvalidOperationException) {
            Console.Write(String.Empty);
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        } else if (numberinfo["number"].AsString().IndexOf('-') == 0) {
          Assert.AreEqual(-1, cbornumber.Sign);
        } else if (numberinfo["number"].AsString().Equals("0")) {
          Assert.AreEqual(0, cbornumber.Sign);
        } else {
          Assert.AreEqual(1, cbornumber.Sign);
        }
      }
    }
    [Test]
    public void TestSimpleValue() {
      // not implemented yet
    }
    [Test]
    public void TestSubtract() {
      try {
        CBORObject.Subtract(null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Subtract(CBORObject.FromObject(2), null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Subtract(CBORObject.Null, CBORObject.FromObject(2));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        CBORObject.Subtract(CBORObject.FromObject(2), CBORObject.Null);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestToJSONString() {
      {
        string stringTemp = CBORObject.FromObject(
        "\u2027\u2028\u2029\u202a\u0008\u000c").ToJSONString();
        Assert.AreEqual(
        "\"\u2027\\u2028\\u2029\u202a\\b\\f\"",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.FromObject(
        "\u0085\ufeff\ufffe\uffff").ToJSONString();
        Assert.AreEqual(
        "\"\\u0085\\uFEFF\\uFFFE\\uFFFF\"",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.True.ToJSONString();
        Assert.AreEqual(
        "true",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.False.ToJSONString();
        Assert.AreEqual(
        "false",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.Null.ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject(Single.PositiveInfinity).ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject(Single.NegativeInfinity).ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.FromObject(Single.NaN).ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject(Double.PositiveInfinity).ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      {
        string stringTemp =
          CBORObject.FromObject(Double.NegativeInfinity).ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.FromObject(Double.NaN).ToJSONString();
        Assert.AreEqual(
        "null",
        stringTemp);
      }
      // Base64 tests
      CBORObject o;
      o = CBORObject.FromObjectAndTag(
        new byte[] { 0x9a, 0xd6, 0xf0, 0xe8 }, 22);
      {
        string stringTemp = o.ToJSONString();
        Assert.AreEqual(
        "\"mtbw6A\"",
        stringTemp);
      }
      o = CBORObject.FromObject(new byte[] { 0x9a, 0xd6, 0xf0, 0xe8 });
      {
        string stringTemp = o.ToJSONString();
        Assert.AreEqual(
        "\"mtbw6A\"",
        stringTemp);
      }
      o = CBORObject.FromObjectAndTag(
        new byte[] { 0x9a, 0xd6, 0xf0, 0xe8 },
        23);
      {
        string stringTemp = o.ToJSONString();
        Assert.AreEqual(
        "\"9AD6F0E8\"",
        stringTemp);
      }
      o = CBORObject.FromObject(new byte[] { 0x9a, 0xd6, 0xff, 0xe8 });
      // Encode with Base64URL by default
      {
        string stringTemp = o.ToJSONString();
        Assert.AreEqual(
        "\"mtb_6A\"",
        stringTemp);
      }
      o = CBORObject.FromObjectAndTag(
        new byte[] { 0x9a, 0xd6, 0xff, 0xe8 },
        22);
      // Encode with Base64
      {
        string stringTemp = o.ToJSONString();
        Assert.AreEqual(
        "\"mtb/6A\"",
        stringTemp);
      }
    }
    [Test]
    public void TestToString() {
      {
        string stringTemp = CBORObject.Undefined.ToString();
        Assert.AreEqual(
        "undefined",
        stringTemp);
      }
      {
        string stringTemp = CBORObject.FromSimpleValue(50).ToString();
        Assert.AreEqual(
        "simple(50)",
        stringTemp);
      }
    }
    [Test]
    public void TestType() {
      // not implemented yet
    }
    [Test]
    public void TestUntag() {
      CBORObject o = CBORObject.FromObjectAndTag("test", 999);
      Assert.AreEqual((BigInteger)999, o.InnermostTag);
      o = o.Untag();
      Assert.AreEqual((BigInteger)(-1), o.InnermostTag);
    }
    [Test]
    public void TestUntagOne() {
      // not implemented yet
    }
    [Test]
    public void TestValues() {
      // not implemented yet
    }
    [Test]
    public void TestWrite() {
      // not implemented yet
    }
    [Test]
    public void TestWriteJSON() {
      // not implemented yet
    }
    [Test]
    public void TestWriteJSONTo() {
      // not implemented yet
    }
    [Test]
    public void TestWriteTo() {
      // not implemented yet
    }
  }
}
