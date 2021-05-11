using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class CitiesGraphV2<TValue, TLabel> where TValue : ListOfContracts<Contract>
    {
        public class Vertex
        {
            public TLabel label;
            public TValue data;
            public List<Vertex> neighbors;

            public Vertex(TLabel label, TValue data)
            {
                this.data = data;
                this.label = label;
                this.neighbors = new List<Vertex>();
            }

            public void RemoveFirstContract()
            {
                data.RemoveFirst();
            }


            //név ??
            public Contract GetCurrentContract()
            {
                return data.GetFirst();
            }
        }

        public List<Vertex> vertices;

        public void RemoveFirstContractFromVertex(Vertex vertex) 
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].label.Equals(vertex.label))
                {
                    vertices[i].RemoveFirstContract();
                }
            }
        }

        public void AddVertex(TValue data, TLabel label)
        {
            Vertex newVertex = new Vertex(label, data);
            vertices.Add(newVertex);
        }

        public void AddEdge(TLabel from, TLabel to)
        {
            Vertex fromVertex = null;
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(from))
                {
                    fromVertex = v;
                    break;
                }
            }
            Vertex toVertex = null;
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(to))
                {
                    toVertex = v;
                    break;
                }
            }
            fromVertex.neighbors.Add(toVertex);
            toVertex.neighbors.Add(fromVertex);
        }

        public List<TLabel> GetNeighbors(TLabel label)
        {
            Vertex vertex = null;
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(label))
                {
                    vertex = v;
                    break;
                }
            }
            List<TLabel> result = new List<TLabel>();
            foreach (Vertex v in vertex.neighbors)
            {
                result.Add(v.label);
            }
            return result;
        }

        public CitiesGraphV2()
        {
            vertices = new List<Vertex>();
        }

        public Vertex GetVertexFromLabel(TLabel label)
        {
            Vertex vertex = null;
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(label))
                {
                    vertex = v;
                    break;
                }
            }
            return vertex;
        }

        public List<Vertex> GetNeighborVertices(TLabel label)
        {
            Vertex vertex = null;
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(label))
                {
                    vertex = v;
                    break;
                }
            }
            List<Vertex> result = new List<Vertex>();
            foreach (Vertex v in vertex.neighbors)
            {
                result.Add(v);
            }
            return result;
        }
    }
}
