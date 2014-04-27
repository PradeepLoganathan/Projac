﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using Projac.Tests.Framework;

namespace Projac.Tests
{
    [TestFixture]
    public class TSqlTests
    {
        [Test]
        public void NullReturnsSqlNullValueInstance()
        {
            Assert.That(TSql.Null(), Is.SameAs(TSqlNullValue.Instance));
        }

        [Test]
        public void VarCharReturnsExpectedInstance()
        {
            Assert.That(TSql.VarChar("value", 123), Is.EqualTo(new TSqlVarCharValue("value", new TSqlVarCharSize(123))));
        }

        [Test]
        public void VarCharNullReturnsExpectedInstance()
        {
            Assert.That(TSql.VarChar(null, 123), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void CharReturnsExpectedInstance()
        {
            Assert.That(TSql.Char("value", 123), Is.EqualTo(new TSqlCharValue("value", new TSqlCharSize(123))));
        }

        [Test]
        public void CharNullReturnsExpectedInstance()
        {
            Assert.That(TSql.Char(null, 123), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void VarCharMaxReturnsExpectedInstance()
        {
            Assert.That(TSql.VarCharMax("value"), Is.EqualTo(new TSqlVarCharValue("value", TSqlVarCharSize.Max)));
        }

        [Test]
        public void VarCharMaxNullReturnsExpectedInstance()
        {
            Assert.That(TSql.VarCharMax(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void NVarCharReturnsExpectedInstance()
        {
            Assert.That(TSql.NVarChar("value", 123), Is.EqualTo(new TSqlNVarCharValue("value", new TSqlNVarCharSize(123))));
        }

        [Test]
        public void NVarCharNullReturnsExpectedInstance()
        {
            Assert.That(TSql.NVarChar(null, 123), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void NCharReturnsExpectedInstance()
        {
            Assert.That(TSql.NChar("value", 123), Is.EqualTo(new TSqlNCharValue("value", new TSqlNCharSize(123))));
        }

        [Test]
        public void NCharNullReturnsExpectedInstance()
        {
            Assert.That(TSql.NChar(null, 123), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void NVarCharMaxReturnsExpectedInstance()
        {
            Assert.That(TSql.NVarCharMax("value"), Is.EqualTo(new TSqlNVarCharValue("value", TSqlNVarCharSize.Max)));
        }

        [Test]
        public void NVarCharMaxNullReturnsExpectedInstance()
        {
            Assert.That(TSql.NVarCharMax(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void BigIntReturnsExpectedInstance()
        {
            Assert.That(TSql.BigInt(123), Is.EqualTo(new TSqlBigIntValue(123)));
        }

        [Test]
        public void BigIntNullReturnsExpectedInstance()
        {
            Assert.That(TSql.BigInt(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void IntReturnsExpectedInstance()
        {
            Assert.That(TSql.Int(123), Is.EqualTo(new TSqlIntValue(123)));
        }

        [Test]
        public void IntNullReturnsExpectedInstance()
        {
            Assert.That(TSql.Int(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void BitReturnsExpectedInstance()
        {
            Assert.That(TSql.Bit(true), Is.EqualTo(new TSqlBitValue(true)));
        }

        [Test]
        public void BitNullReturnsExpectedInstance()
        {
            Assert.That(TSql.Bit(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void UniqueIdentifierReturnsExpectedInstance()
        {
            Assert.That(TSql.UniqueIdentifier(Guid.Empty), Is.EqualTo(new TSqlUniqueIdentifierValue(Guid.Empty)));
        }

        [Test]
        public void UniqueIdentifierNullReturnsExpectedInstance()
        {
            Assert.That(TSql.UniqueIdentifier(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void BinaryReturnsExpectedInstance()
        {
            Assert.That(TSql.Binary(new byte[]{ 1,2,3}, 123), Is.EqualTo(new TSqlBinaryValue(new byte[] { 1, 2 , 3}, new TSqlBinarySize(123))));
        }

        [Test]
        public void BinaryNullReturnsExpectedInstance()
        {
            Assert.That(TSql.Binary(null, 123), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void VarBinaryReturnsExpectedInstance()
        {
            Assert.That(TSql.VarBinary(new byte[] { 1, 2, 3 }, 123), Is.EqualTo(new TSqlVarBinaryValue(new byte[] { 1, 2, 3 }, new TSqlVarBinarySize(123))));
        }

        [Test]
        public void VarBinaryNullReturnsExpectedInstance()
        {
            Assert.That(TSql.VarBinary(null, 123), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void VarBinaryMaxReturnsExpectedInstance()
        {
            Assert.That(TSql.VarBinaryMax(new byte[] { 1, 2, 3 }), Is.EqualTo(new TSqlVarBinaryValue(new byte[] { 1, 2, 3 }, TSqlVarBinarySize.Max)));
        }

        [Test]
        public void VarBinaryMaxNullReturnsExpectedInstance()
        {
            Assert.That(TSql.VarBinaryMax(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [Test]
        public void DateTimeOffsetReturnsExpectedInstance()
        {
            var value = DateTimeOffset.UtcNow;
            Assert.That(TSql.DateTimeOffset(value), Is.EqualTo(new TSqlDateTimeOffsetValue(value)));
        }

        [Test]
        public void DateTimeOffsetNullReturnsExpectedInstance()
        {
            Assert.That(TSql.DateTimeOffset(null), Is.EqualTo(TSqlNullValue.Instance));
        }

        [TestCaseSource("NonQueryCases")]
        public void NonQueryReturnsExpectedInstance(TSqlNonQueryStatement actual, TSqlNonQueryStatement expected)
        {
            Assert.That(actual.Text, Is.EqualTo(expected.Text));
            Assert.That(actual.Parameters, Is.EquivalentTo(expected.Parameters).Using(new SqlParameterEqualityComparer()));
        }

        private static IEnumerable<TestCaseData> NonQueryCases()
        {
            yield return new TestCaseData(
                TSql.NonQuery("text"),
                new TSqlNonQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.NonQuery("text", parameters: null),
                new TSqlNonQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.NonQuery("text", new { }),
                new TSqlNonQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.NonQuery("text", new { Parameter = TSql.Bit(true) }),
                new TSqlNonQueryStatement("text", new []
                {
                    new TSqlBitValue(true).ToSqlParameter("@Parameter")
                }));
            yield return new TestCaseData(
                TSql.NonQuery("text", new { Parameter1 = TSql.Bit(true), Parameter2 = TSql.Bit(null) }),
                new TSqlNonQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@Parameter1"),
                    TSqlNullValue.Instance.ToSqlParameter("@Parameter2")
                }));
        }

        [TestCaseSource("NonQueryFormatCases")]
        public void NonQueryFormatReturnsExpectedInstance(TSqlNonQueryStatement actual, TSqlNonQueryStatement expected)
        {
            Assert.That(actual.Text, Is.EqualTo(expected.Text));
            Assert.That(actual.Parameters, Is.EquivalentTo(expected.Parameters).Using(new SqlParameterEqualityComparer()));
        }

        private static IEnumerable<TestCaseData> NonQueryFormatCases()
        {
            yield return new TestCaseData(
                TSql.NonQueryFormat("text"),
                new TSqlNonQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.NonQueryFormat("text", parameters: null),
                new TSqlNonQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.NonQueryFormat("text", new ITSqlParameterValue[0]),
                new TSqlNonQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.NonQueryFormat("text", TSql.Bit(true)),
                new TSqlNonQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0")
                }));
            yield return new TestCaseData(
                TSql.NonQueryFormat("text {0}", TSql.Bit(true)),
                new TSqlNonQueryStatement("text @P0", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0")
                }));
            yield return new TestCaseData(
                TSql.NonQueryFormat("text", TSql.Bit(true), TSql.Bit(null)),
                new TSqlNonQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0"),
                    TSqlNullValue.Instance.ToSqlParameter("@P1")
                }));
            yield return new TestCaseData(
                TSql.NonQueryFormat("text {0} {1}", TSql.Bit(true), TSql.Bit(null)),
                new TSqlNonQueryStatement("text @P0 @P1", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0"),
                    TSqlNullValue.Instance.ToSqlParameter("@P1")
                }));
        }

        [TestCaseSource("QueryCases")]
        public void QueryReturnsExpectedInstance(TSqlQueryStatement actual, TSqlQueryStatement expected)
        {
            Assert.That(actual.Text, Is.EqualTo(expected.Text));
            Assert.That(actual.Parameters, Is.EquivalentTo(expected.Parameters).Using(new SqlParameterEqualityComparer()));
        }

        private static IEnumerable<TestCaseData> QueryCases()
        {
            yield return new TestCaseData(
                TSql.Query("text"),
                new TSqlQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.Query("text", parameters: null),
                new TSqlQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.Query("text", new { }),
                new TSqlQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.Query("text", new { Parameter = TSql.Bit(true) }),
                new TSqlQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@Parameter")
                }));
            yield return new TestCaseData(
                TSql.Query("text", new { Parameter1 = TSql.Bit(true), Parameter2 = TSql.Bit(null) }),
                new TSqlQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@Parameter1"),
                    TSqlNullValue.Instance.ToSqlParameter("@Parameter2")
                }));
        }

        [TestCaseSource("QueryFormatCases")]
        public void QueryFormatReturnsExpectedInstance(TSqlQueryStatement actual, TSqlQueryStatement expected)
        {
            Assert.That(actual.Text, Is.EqualTo(expected.Text));
            Assert.That(actual.Parameters, Is.EquivalentTo(expected.Parameters).Using(new SqlParameterEqualityComparer()));
        }

        private static IEnumerable<TestCaseData> QueryFormatCases()
        {
            yield return new TestCaseData(
               TSql.QueryFormat("text"),
               new TSqlQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.QueryFormat("text", parameters: null),
                new TSqlQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.QueryFormat("text", new ITSqlParameterValue[0]),
                new TSqlQueryStatement("text", new SqlParameter[0]));
            yield return new TestCaseData(
                TSql.QueryFormat("text", TSql.Bit(true)),
                new TSqlQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0")
                }));
            yield return new TestCaseData(
                TSql.QueryFormat("text {0}", TSql.Bit(true)),
                new TSqlQueryStatement("text @P0", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0")
                }));
            yield return new TestCaseData(
                TSql.QueryFormat("text", TSql.Bit(true), TSql.Bit(null)),
                new TSqlQueryStatement("text", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0"),
                    TSqlNullValue.Instance.ToSqlParameter("@P1")
                }));
            yield return new TestCaseData(
                TSql.QueryFormat("text {0} {1}", TSql.Bit(true), TSql.Bit(null)),
                new TSqlQueryStatement("text @P0 @P1", new[]
                {
                    new TSqlBitValue(true).ToSqlParameter("@P0"),
                    TSqlNullValue.Instance.ToSqlParameter("@P1")
                }));
        }

        [Test]
        public void ComposeStatementArrayCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => TSql.Compose((TSqlNonQueryStatement[])null));
        }

        [Test]
        public void ComposeStatementEnumerationCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => TSql.Compose((IEnumerable<TSqlNonQueryStatement>)null));
        }

        [Test]
        public void ComposeStatementArrayReturnsComposer()
        {
            Assert.IsInstanceOf<TSqlNonQueryStatementComposer>(
                TSql.Compose(
                    StatementFactory(),
                    StatementFactory()));
        }
        [Test]
        public void ComposeStatementEnumerationReturnsComposer()
        {
            Assert.IsInstanceOf<TSqlNonQueryStatementComposer>(
                TSql.Compose((IEnumerable<TSqlNonQueryStatement>)new[]
                {
                    StatementFactory(),
                    StatementFactory()
                }));
        }

        [Test]
        public void ComposedStatementArrayIsPreservedAndReturnedByComposer()
        {
            var statement1 = StatementFactory();
            var statement2 = StatementFactory();

            TSqlNonQueryStatement[] result = TSql.Compose(statement1, statement2);

            Assert.That(result, Is.EquivalentTo(new []
            {
                statement1, statement2
            }));
        }

        [Test]
        public void ComposedStatementEnumerationIsPreservedAndReturnedByComposer()
        {
            var statement1 = StatementFactory();
            var statement2 = StatementFactory();

            TSqlNonQueryStatement[] result = TSql.Compose((IEnumerable<TSqlNonQueryStatement>)new[]
            {
                statement1, statement2
            });

            Assert.That(result, Is.EquivalentTo(new[]
            {
                statement1, statement2
            }));
        }

        private static TSqlNonQueryStatement StatementFactory()
        {
            return new TSqlNonQueryStatement("text", new SqlParameter[0]);
        }

        [Test]
        public void ProjectionReturnsBuilder()
        {
            var result = TSql.Projection();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<TSqlProjectionBuilder>());
        }

        [Test]
        public void ProjectionReturnsNewBuilderUponEachCall()
        {
            var result1 = TSql.Projection();
            var result2 = TSql.Projection();

            Assert.That(result1, Is.Not.SameAs(result2));
        }
    }
}
