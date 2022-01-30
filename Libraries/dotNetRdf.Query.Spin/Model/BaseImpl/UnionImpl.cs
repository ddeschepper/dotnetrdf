/*
// <copyright>
// dotNetRDF is free and open source software licensed under the MIT License
// -------------------------------------------------------------------------
// 
// Copyright (c) 2009-2021 dotNetRDF Project (http://dotnetrdf.org/)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
*/

using System.Collections.Generic;
using VDS.RDF.Query.Spin.SparqlUtil;

namespace VDS.RDF.Query.Spin.Model
{
    internal class UnionImpl : ElementImpl, IUnion
    {

        public UnionImpl(INode node, IGraph graph, SpinProcessor spinModel)
            : base(node, graph, spinModel)
        {

        }


        override public void Print(ISparqlPrinter p)
        {
            List<IElement> elements = getElements();
            for (IEnumerator<IElement> it = elements.GetEnumerator(); it.MoveNext(); )
            {
                IElement element = it.Current;
                p.print("{");
                p.println();
                p.setIndentation(p.getIndentation() + 1);
                element.Print(p);
                p.setIndentation(p.getIndentation() - 1);
                p.printIndentation(p.getIndentation());
                p.print("}");
                if (it.MoveNext())
                {
                    p.println();
                    p.printIndentation(p.getIndentation());
                    p.printKeyword("UNION");
                    p.println();
                    p.printIndentation(p.getIndentation());
                }
            }
        }


        //override public void visit(IElementVisitor visitor)
        //{
        //    visitor.visit(this);
        //}
    }
}