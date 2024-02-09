using BoostDraft.XmlUtils;

namespace BoostDraft.Tests
{
    public class XmlHelperTests
    {
        [Test]
        public void GivenValidXmlString_DetermineXml_ReturnsTrue()
        {
            Assert.IsTrue(XmlHelper.DetermineXml("<Design><Code>hello world</Code></Design>"));
            Assert.IsTrue(XmlHelper.DetermineXml("<Design></Design>"));
            Assert.IsTrue(XmlHelper.DetermineXml("<Design>hello world</Design>"));
        }
        
        [Test]
        public void GivenXmlString_WithMultipleNestedElements_DetermineXml_ReturnsTrue()
        {
            Assert.IsTrue(XmlHelper.DetermineXml("<Design><Code>hello world</Code><Code2>hello world2</Code2></Design>"));
        }

        [Test]
        public void GivenXmlString_WithMultipleRepeatedNestedElements_DetermineXml_ReturnsTrue()
        {
            Assert.IsTrue(XmlHelper.DetermineXml("<Design><Code>hello world</Code><Code>hello world</Code></Design>"));
        }

        [Test]
        public void GivenXmlString_WithExtraElement_DetermineXml_ReturnsFalse()
        {
            Assert.IsFalse(XmlHelper.DetermineXml("<Design><Code>hello world</Code></Design><People>"));
        }

        [Test]
        public void GivenXmlString_UnorderedEndElements_DetermineXml_ReturnsFalse()
        {
            Assert.IsFalse(XmlHelper.DetermineXml("<People><Design><Code>hello world</People></Code></Design>"));
        }

        [Test]
        public void GivenXmlString_WithMismatchingAttributes_DetermineXml_ReturnsFalse()
        {
            Assert.IsFalse(XmlHelper.DetermineXml("<People age=”1”>hello world</People>"));
        }

        [Test]
        public void GivenXmlString_WithMalformedTags_ReturnsFalse()
        {
            Assert.IsFalse(XmlHelper.DetermineXml("Design<Code>hello world</Code>/Design"));
            Assert.IsFalse(XmlHelper.DetermineXml(">Design><Code>hello world</Code></Design<"));
            Assert.IsFalse(XmlHelper.DetermineXml("<Design>>Code>hello world</Code></Design>"));
            Assert.IsFalse(XmlHelper.DetermineXml("<Design><Code<hello world</Code></Design>"));
            Assert.IsFalse(XmlHelper.DetermineXml("<Design><Code>hello world<Code/></Design>"));
            Assert.IsFalse(XmlHelper.DetermineXml("<Design><Code>hello world</Code><Design/>"));
            Assert.IsFalse(XmlHelper.DetermineXml("<Design><Design>"));
            Assert.IsFalse(XmlHelper.DetermineXml("</Design></Design>"));
            Assert.IsFalse(XmlHelper.DetermineXml("hello world"));
        }
    }
}