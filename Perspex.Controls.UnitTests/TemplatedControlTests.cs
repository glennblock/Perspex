﻿// -----------------------------------------------------------------------
// <copyright file="TemplatedControlTests.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.Controls.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Perspex.Controls;
    using Perspex.Controls.Primitives;
    using Perspex.VisualTree;

    [TestClass]
    public class TemplatedControlTests
    {
        [TestMethod]
        public void Template_Doesnt_Get_Executed_On_Set()
        {
            bool executed = false;
            
            var template = new ControlTemplate(_ => 
            {
                executed = true;
                return new Control();
            });

            var target = new TemplatedControl
            {
                Template = template,
            };

            Assert.IsFalse(executed);
        }

        [TestMethod]
        public void Template_Gets_Executed_On_Measure()
        {
            bool executed = false;

            var template = new ControlTemplate(_ =>
            {
                executed = true;
                return new Control();
            });

            var target = new TemplatedControl
            {
                Template = template,
            };

            target.Measure(new Size(100, 100));

            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void Template_Result_Becomes_Visual_Child()
        {
            Control templateResult = new Control();

            var template = new ControlTemplate(_ =>
            {
                return templateResult;
            });

            var target = new TemplatedControl
            {
                Template = template,
            };

            target.Measure(new Size(100, 100));
            var children = target.GetVisualChildren().ToList();

            CollectionAssert.AreEqual(new[] { templateResult }, children);
        }

        [TestMethod]
        public void TemplatedParent_Is_Set_On_Generated_Template()
        {
            Control templateResult = new Control();

            var template = new ControlTemplate(_ =>
            {
                return templateResult;
            });

            var target = new TemplatedControl
            {
                Template = template,
            };

            target.Measure(new Size(100, 100));

            Assert.AreEqual(target, templateResult.TemplatedParent);
        }

        [TestMethod]
        public void OnTemplateApplied_Is_Called()
        {
            var target = new TestTemplatedControl
            {
                Template = new ControlTemplate(_ =>
                {
                    return new Control();
                })
            };

            target.Measure(new Size(100, 100));

            Assert.IsTrue(target.OnTemplateAppliedCalled);
        }
    }
}

