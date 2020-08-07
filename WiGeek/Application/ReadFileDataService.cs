using Castle.Core.Internal;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WiGeek.Domain.WardAggregate;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Application
{
    public class ReadFileDataService: IReadFileDataService
    {
        private readonly List<string> list = new List<string>();
        public ReadFileDataService()
        {
            list.Add("病区字典");
            list.Add("工作字典");
            list.Add("婚姻字典");
            list.Add("就诊记录");
            list.Add("科室字典");
            list.Add("医嘱项目类型字典");
            list.Add("诊断数据");
            list.Add("医嘱数据");
            list.Add("体征数据");
            list.Add("医嘱状态字典");
        }
        public FileInfo[] ReadFileInfos(DirectoryInfo directoryInfo)
        {
            var fileInfos = new List<FileInfo>();
            fileInfos.AddRange(directoryInfo.GetFiles());
            foreach (var directory in directoryInfo.GetDirectories())
            {
                fileInfos.AddRange(ReadFileInfos(directory));
            }
            return fileInfos.ToArray();
        }

        private void ReadFile(string path)
        { 
            var directoryInfo = new DirectoryInfo(path);
            var fileInfos = ReadFileInfos(directoryInfo);
            var files =fileInfos.Where(x => list.Any(y => x.Name.Contains(y)));
            //readData<Ward>()
        }

        public IEnumerable<T> readData<T>(string FilePath) where T:class ,new ()
        {
            var list = new List<T>();
            Dictionary<int, PropertyInfo> PropertyInfoCols = new Dictionary<int, PropertyInfo>();
            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var colNumberAttribute = property.GetAttribute<ColNumberAttribute>();
                if (colNumberAttribute != null)
                {
                    PropertyInfoCols.Add(colNumberAttribute.ColNumber, property);
                }
            }
            bool fisrt = true;
            using (TextFieldParser parser = new TextFieldParser(FilePath, Encoding.GetEncoding("GB2312")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    T obj = new T();
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (fisrt)
                            continue;
                        var field = fields[i];
                        if (PropertyInfoCols.ContainsKey(i))
                        {
                            PropertyInfoCols[i].SetValue(obj, field);
                        }
                    }
                    if (!fisrt)
                        list.Add(obj);
                    //yield return obj;
                    fisrt = false;
                }
            }
            return list;
        }
    }
}
