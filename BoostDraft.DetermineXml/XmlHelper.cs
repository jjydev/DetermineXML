namespace BoostDraft.XmlUtils
{
    public static class XmlHelper
    {
        private const string tagStart = "<";
        private const string tagEnd = ">";
        private const string endElement = "/";

        public static bool DetermineXml(string xml)
        {
            var stack = new Stack<string>();
            var start = 0;
            while (start < xml.Length)
            {
                var end = xml.AsSpan(start).IndexOfAny(tagEnd);
                var element = xml.AsSpan(start, end + 1);

                if (IsEndElement(element))
                {
                    if (!stack.Any())
                        return false;
                    if (element.ElementName().CompareTo(stack.Pop().AsSpan(), StringComparison.OrdinalIgnoreCase) != 0)
                        return false;
                }

                else if (IsStartElement(element))
                {
                    stack.Push(element.ElementName().ToString());
                }

                else // content
                {
                    if (!stack.Any())
                        return false;

                    // ignore content and proceed to the next element
                    var nextElement = xml.AsSpan(start).IndexOfAny(tagStart);
                    start += nextElement;
                    continue;
                }

                start += end + 1;
            }

            if (!stack.Any())
                return true;
            return false;
        }

        private static bool IsStartElement(ReadOnlySpan<char> element) => element.StartsWith(tagStart);

        private static bool IsEndElement(ReadOnlySpan<char> element) => element.StartsWith($"{tagStart}{endElement}");

        private static ReadOnlySpan<char> ElementName(this ReadOnlySpan<char> element)
        {
            return element.Trim(tagStart)
                .TrimStart($"{tagStart}{endElement}")
                .TrimEnd(tagEnd);
        }
    }
}