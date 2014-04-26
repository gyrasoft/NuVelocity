// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

using System.Collections.Generic;

namespace NVelocity.Test
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Text.RegularExpressions;
	using App;
	using NUnit.Framework;

    /// <summary>
    /// Tests to make sure that inherited properties / methods are functioning correctly
    /// </summary>

    class ParentNonVirtual
    {
        private string _name;
        public string Name { get { return _name; } }

        public ParentNonVirtual()
        {
            _name = "ParentNonVirtual";
        }
    }

    class ChildHidesParent : ParentNonVirtual
    {
        private string _childname;
        public new string Name { get { return _childname; } }

        public ChildHidesParent()
        {
            _childname = "ChildHidesParent";
        }
    }

    class MyContainerBase
    {
        private List<ParentNonVirtual> _items;

        public List<ParentNonVirtual> items { get { return _items; } }

        public MyContainerBase()
        {
            _items = new List<ParentNonVirtual>();
            _items.Add(new ParentNonVirtual());
            _items.Add(new ParentNonVirtual());
        }
    }

    class MyContainer : MyContainerBase
    {
        private List<ChildHidesParent> _childitems;
        
        new public List<ChildHidesParent> items { get { return _childitems; } }

        public MyContainer()
        {
            _childitems = new List<ChildHidesParent>();
            _childitems.Add(new ChildHidesParent());
            _childitems.Add(new ChildHidesParent());
        }
    }


	[TestFixture]
	public class InheritanceTest
	{
		private ArrayList items;
	    private VelocityContext c;
		private StringWriter sw;
		private VelocityEngine velocityEngine;
		private Boolean ok;

		[SetUp]
		public void Setup()
		{
			items = new ArrayList();
            items.Add(new ParentNonVirtual());
            items.Add(new ChildHidesParent());

			c = new VelocityContext();
            c.Put("pnv", new ParentNonVirtual());
            c.Put("chp", new ChildHidesParent());
            c.Put("items", items);
            c.Put("mycontainer", new MyContainer());

			sw = new StringWriter();

			velocityEngine = new VelocityEngine();
			velocityEngine.Init();
		}

		[Test]
        public void HideBaseProperty()
		{
			ok = velocityEngine.Evaluate(c, sw,
			                             "ContextTest.CaseInsensitive",
			                             @"
						$chp.Name
				");

			Assert.IsTrue(ok, "Evaluation returned failure");
            Assert.AreEqual("ChildHidesParent", Normalize(sw));
		}

        [Test]
		public void ForEachContainerOfBaseWithDerivedInstances()
        {
            ok = velocityEngine.Evaluate(c, sw,
                                         "ContextTest.CaseInsensitive",
                                         @"
					#foreach( $item in $items )
						$item.Name,
					#end
				");

            Assert.IsTrue(ok, "Evaluation returned failure");
            Assert.AreEqual("ParentNonVirtual,ChildHidesParent,", Normalize(sw));
        }

        // This is the tricky one, a specific unit test for the bug fix where Velocity originally
        // would possibly choose the parent property. If a child declares a property that is a generic
        // collection with the same name as parent property, but with different type, NVelocity
        // was actually choosing the parent collection and iterating parent types. The behavior does
        // not show up for simple properties, only collections. --Melvin Smith
        [Test]
        public void ForEachGenericContainerOfChild()
        {
            ok = velocityEngine.Evaluate(c, sw,
                                         "ContextTest.CaseInsensitive",
                                         @"
					#foreach( $item in $mycontainer.items )
						$item.Name,
					#end
				");

            Assert.IsTrue(ok, "Evaluation returned failure");
            Assert.AreEqual("ChildHidesParent,ChildHidesParent,", Normalize(sw));
        }


		private string Normalize(StringWriter sw)
		{
			return Regex.Replace(sw.ToString(), "\\s+", string.Empty);
		}
	}
}