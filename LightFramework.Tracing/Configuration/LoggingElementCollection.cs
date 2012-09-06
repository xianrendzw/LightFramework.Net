using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LightFramework.Tracing
{
    //[ConfigurationCollection(typeof(LoggingElement), AddItemName = "logging")]
    //public class LoggingElementCollection : ConfigurationElementCollection
    //{
    //    public new LoggingElement this[string name]
    //    {
    //        get { return (LoggingElement)base.BaseGet(name); }
    //        set
    //        {
    //            if (base.BaseGet(name) != null)
    //            {
    //                int index = base.BaseIndexOf(base.BaseGet(name));
    //                base.BaseRemoveAt(index);
    //                base.BaseAdd(index, value);
    //                return;
    //            }
    //            this.BaseAdd(value);
    //        }
    //    }

    //    public LoggingElement this[int index]
    //    {
    //        get { return (LoggingElement)base.BaseGet(index); }
    //        set
    //        {
    //            if (base.BaseGet(index) != null)
    //            {
    //                base.BaseRemoveAt(index);
    //            }
    //            this.BaseAdd(index, value);
    //        }
    //    }

    //    public void Add(LoggingElement element)
    //    {
    //        this[element.Name] = element;
    //    }

    //    public void Remove(string key)
    //    {
    //        if (base.BaseGet(key) != null)
    //            base.BaseRemove(key);
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new LoggingElement();
    //    }

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((LoggingElement)element).Name;
    //    }
    //}
}
