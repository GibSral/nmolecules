﻿using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace NMolecules.Analyzers.Test
{
    public static class SampleDataLoader
    {
        private static readonly Lazy<(string attributesCode, int attributesLineCount)> Attributes = new(() =>
        {
            var type = typeof(SampleDataLoader);
            var assembly = type.Assembly;
            var resourcePath = $"{type.Namespace}.Attributes.cs";
            var attributes = LoadResource(assembly, resourcePath);

            return (attributes, attributes.LineCount());
        });

        private static int LineCount(this string str)
        {
            return str.Split('\n').Length;
        }

        public static string GetAttributes()
        {
            return Attributes.Value.attributesCode;
        }

        public static string  LoadFromNamespaceOf<T>(string sampleName)
        {
            var stringBuilder = new StringBuilder();
            var type = typeof(T);
            var resourcePath = $"{type.Namespace!}.SampleData.{sampleName}";
            var assembly = type.Assembly;
            var sampleData = LoadResource(assembly!, resourcePath!);
            stringBuilder.AppendLine(sampleData);
            return stringBuilder.ToString();
        }

        private static string LoadResource(Assembly assembly, string resourcePath)
        {
            var manifestResourceStream = assembly.GetManifestResourceStream(resourcePath)!;
            using var sr = new StreamReader(manifestResourceStream);
            return sr.ReadToEnd();
        }
    }
}