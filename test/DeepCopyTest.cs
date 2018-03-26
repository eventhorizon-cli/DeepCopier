using System.Linq;
using Xunit;
using DeepCopier.Test.TestClasses;
using DeepCopier;
using System.Collections.Generic;

namespace test
{
    public class DeepCopierTest
    {
        /// <summary>
        /// 测试拷贝一个对象本身
        /// </summary>
        [Fact]
        public void TestSelfCopy()
        {
            ClassA a = new ClassA
            {
                ValueTypeProp = 123,
                StringProp = "test"
            };

            ClassA a2 = Copier.Copy(a);

            Assert.Equal(a.ValueTypeProp, a2.ValueTypeProp);
            Assert.Equal(a.StringProp, a2.StringProp);
        }

        /// <summary>
        /// 测试拷贝简单的属性
        /// </summary>
        [Fact]
        public void TestSimpleProperties()
        {
            ClassA a = new ClassA
            {
                ValueTypeProp = 123,
                StringProp = "test"
            };

            ClassB b = Copier.Copy<ClassA, ClassB>(a);

            Assert.Equal(a.ValueTypeProp, b.ValueTypeProp);
            Assert.Equal(a.StringProp, b.StringProp);
        }

        /// <summary>
        /// 测试拷贝引用类型的属性
        /// </summary>
        [Fact]
        public void TestRefTypeProperties()
        {
            ClassB b = new ClassB
            {
                ValueTypeProp = 1,
                StringProp = "string1",
                ClassATypeProp = new ClassA
                {
                    ValueTypeProp = 2,
                    StringProp = "string2",
                }
            };

            ClassC c = Copier.Copy<ClassB, ClassC>(b);

            Assert.Equal(b.ValueTypeProp, c.ValueTypeProp);
            Assert.Equal(b.StringProp, c.StringProp);
            Assert.Equal(b.ClassATypeProp.ValueTypeProp, c.ClassATypeProp.ValueTypeProp);
            Assert.Equal(b.ClassATypeProp.StringProp, c.ClassATypeProp.StringProp);
            Assert.NotSame(b.ClassATypeProp, c.ClassATypeProp);
        }

        /// <summary>
        /// 测试可遍历的属性
        /// </summary>
        [Fact]
        public void TestEnumableProperties()
        {
            // 测试数组的拷贝
            ClassD d = new ClassD
            {
                VuleTypeArray = new int[] { 1, 2, 3 },
                ClassATypeArray = new ClassA[]
                {
                    new ClassA
                    {
                        ValueTypeProp = 1,
                        StringProp = "string1"
                    },
                    new ClassA
                    {
                        ValueTypeProp = 2,
                        StringProp = "string2"
                    }
                }
            };

            ClassE e = Copier.Copy<ClassD, ClassE>(d);
            Assert.Equal(d.VuleTypeArray, e.VuleTypeArray);
            Assert.NotSame(d.VuleTypeArray, e.VuleTypeArray);

            Assert.Equal(d.ClassATypeArray.Select(x => x.ValueTypeProp),
                e.ClassATypeArray.Select(x => x.ValueTypeProp));

            Assert.Equal(d.ClassATypeArray.Select(x => x.StringProp),
                e.ClassATypeArray.Select(x => x.StringProp));

            Assert.NotEqual(d.ClassATypeArray.AsEnumerable(), e.ClassATypeArray.AsEnumerable());

            Assert.NotSame(d.ClassATypeArray, e.ClassATypeArray);

            // 测试List的拷贝
            ClassD d2 = new ClassD
            {
                VuleTypeList = new List<int> { 1, 2, 3 },
                ClassATypeList = new List<ClassA>
                {
                    new ClassA
                    {
                        ValueTypeProp = 1,
                        StringProp = "string1"
                    },
                    new ClassA
                    {
                        ValueTypeProp = 2,
                        StringProp = "string2"
                    }
                }
            };

            ClassE e2 = Copier.Copy<ClassD, ClassE>(d2);
            Assert.Equal(d2.VuleTypeList, e2.VuleTypeList);
            Assert.NotSame(d2.VuleTypeList, e2.VuleTypeList);

            Assert.Equal(d2.ClassATypeList.Select(x => x.ValueTypeProp),
                e2.ClassATypeList.Select(x => x.ValueTypeProp));

            Assert.Equal(d2.ClassATypeList.Select(x => x.StringProp),
                e2.ClassATypeList.Select(x => x.StringProp));

            Assert.NotEqual(d2.ClassATypeList.AsEnumerable(), e2.ClassATypeList.AsEnumerable());

            Assert.NotSame(d2.ClassATypeList, e2.ClassATypeList);
        }
    }
}
