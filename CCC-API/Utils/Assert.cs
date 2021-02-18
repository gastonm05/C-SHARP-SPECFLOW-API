using CCC_Infrastructure.Utils;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

namespace CCC_API.Utils.Assertion
{
    public static class Assert
    {
        public static void Fail(string message)
        {
            NUnit.Framework.Assert.Fail(StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void True(bool condition)
        {
            NUnit.Framework.Assert.True(condition, StackTraceErrorAppender.AddMultipleLines());
        }

        public static void True(bool condition, string message)
        {
            NUnit.Framework.Assert.True(condition, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void IsTrue(bool condition, string message)
        {
            NUnit.Framework.Assert.IsTrue(condition, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void False(bool condition, string message)
        {
            NUnit.Framework.Assert.False(condition, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void IsFalse(bool condition, string message)
        {
            NUnit.Framework.Assert.IsFalse(condition, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void That(bool condition)
        {
            NUnit.Framework.Assert.That(condition, StackTraceErrorAppender.AddMultipleLines());
        }

        public static void That(bool condition, string message)
        {
            NUnit.Framework.Assert.That(condition, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void That<T>(T value, IResolveConstraint constraint)
        {
            string msg = string.Empty;
            switch (constraint.GetType().ToString())
            {
                case "NUnit.Framework.Constraints.EqualConstraint":
                    msg = StackTraceErrorAppender.AddOneLine();
                    break;
                default:
                    msg = StackTraceErrorAppender.AddMultipleLines();
                    break;
            }
            NUnit.Framework.Assert.That(value, constraint, msg);
        }

        public static void That<T>(T value, IResolveConstraint constraint, string message)
        {
            string msg = string.Empty;
            switch (constraint.GetType().ToString())
            {
                case "NUnit.Framework.Constraints.EqualConstraint":
                    msg = StackTraceErrorAppender.AddOneLine(message);
                    break;
                default:
                    msg = StackTraceErrorAppender.AddMultipleLines(message);
                    break;
            }
            NUnit.Framework.Assert.That(value, constraint, msg);
        }

        public static void That(TestDelegate td, IResolveConstraint constraint, string message)
        {
            string msg = string.Empty;
            switch (constraint.GetType().ToString())
            {
                case "NUnit.Framework.Constraints.EqualConstraint":
                    msg = StackTraceErrorAppender.AddOneLine(message);
                    break;
                default:
                    msg = StackTraceErrorAppender.AddMultipleLines(message);
                    break;
            }
            NUnit.Framework.Assert.That(td, constraint, msg);
        }

        public static void AreEqual(IEnumerable expected, IEnumerable actual, string message)
        {
            CollectionAssert.AreEqual(expected, actual, StackTraceErrorAppender.AddOneLine(message));
        }

        public static void AreEquivalent(IEnumerable expected, IEnumerable actual, string message)
        {
            CollectionAssert.AreEquivalent(expected, actual, StackTraceErrorAppender.AddOneLine(message));
        }

        public static void AreEqual<T>(T expected, T actual)
        {
            NUnit.Framework.Assert.AreEqual(expected, actual, StackTraceErrorAppender.AddOneLine());
        }

        public static void AreEqual<T>(T expected, T actual, string message)
        {
            NUnit.Framework.Assert.AreEqual(expected, actual, StackTraceErrorAppender.AddOneLine(message));
        }

        public static void AreNotEqual<T>(T expected, T actual)
        {
            NUnit.Framework.Assert.AreNotEqual(expected, actual, StackTraceErrorAppender.AddOneLine());
        }

        public static void AreNotEqual<T>(T expected, T actual, string message)
        {
            NUnit.Framework.Assert.AreNotEqual(expected, actual, StackTraceErrorAppender.AddOneLine(message));
        }

        public static void NotNull(object obj)
        {
            NUnit.Framework.Assert.NotNull(obj, StackTraceErrorAppender.AddMultipleLines());
        }

        public static void NotNull(object obj, string message)
        {
            NUnit.Framework.Assert.NotNull(obj, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void NotNull(object obj, string message, string extra)
        {
            NUnit.Framework.Assert.NotNull(obj, StackTraceErrorAppender.AddMultipleLines(message + extra));
        }

        public static void IsNull(object obj, string message)
        {
            NUnit.Framework.Assert.IsNull(obj, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void IsNotNull(object obj, string message)
        {
            NUnit.Framework.Assert.IsNotNull(obj, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void IsEmpty(IEnumerable list, string message)
        {
            NUnit.Framework.Assert.IsEmpty(list, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void IsNotEmpty(string value)
        {
            NUnit.Framework.Assert.IsNotEmpty(value, StackTraceErrorAppender.AddMultipleLines());
        }

        public static void IsNotEmpty(string value, string message)
        {
            NUnit.Framework.Assert.IsNotEmpty(value, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void IsNotEmpty(IEnumerable list)
        {
            NUnit.Framework.Assert.IsNotEmpty(list, StackTraceErrorAppender.AddMultipleLines());
        }

        public static void IsNotEmpty(IEnumerable list, string message)
        {
            NUnit.Framework.Assert.IsNotEmpty(list, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void Multiple(TestDelegate td)
        {
            NUnit.Framework.Assert.Multiple(td);
        }

        public static void Contains(string expected, string actual, string message)
        {
            StringAssert.Contains(expected, actual, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void Contains(string actual, IEnumerable collection, string message)
        {
            CollectionAssert.Contains(collection, actual, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void Contains<T>(T actual, IList<T> collection, string message)
        {
            CollectionAssert.Contains(collection, actual, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void Contains<T>(IList<T> collection, T actual, string message)
        {
            CollectionAssert.Contains(collection, actual, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void DoesNotContain<T>(IList<T> collection, T actual, string message)
        {
            CollectionAssert.DoesNotContain(collection, actual, StackTraceErrorAppender.AddMultipleLines(message));
        }

        public static void Ignore(string message)
        {
            NUnit.Framework.Assert.Ignore(message);
        }
    }
}
