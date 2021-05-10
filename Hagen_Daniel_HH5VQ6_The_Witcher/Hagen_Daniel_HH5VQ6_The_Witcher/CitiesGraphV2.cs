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
            //public int level; //new BFSlevel
            //public Vertex BFSparent; //newnew

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
            // meg kelll keresni a csúcsot és abban meghívni a RemoveFirstContract()
            /*foreach (Vertex v in vertices)
            {
                if (v.Equals(vertex))
                {
                    v.RemoveFirstContract();
                    break;
                }
            }*/
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].label.Equals(vertex.label))
                {
                    vertices[i].RemoveFirstContract();
                }
            }
            // label alapján összehasonlítva
            //throw new NotImplementedException();
        }

        public void AddVertex(TValue data, TLabel label)
        {
            Vertex newVertex = new Vertex(label, data);
            /*newVertex.data = data;
            newVertex.label = label;
            newVertex.neighbors = new List<Vertex>();*/
            vertices.Add(newVertex);
        }

        public void AddEdge(TLabel from, TLabel to)
        {
            Vertex fromVertex = null;
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(from))   //v.label==from
                {
                    fromVertex = v;
                    break;
                }
            }
            Vertex toVertex = null; //Add missing vertex exception
            foreach (Vertex v in vertices)
            {
                if (v.label.Equals(to))   //v.label==from
                {
                    toVertex = v;
                    break;
                }
            }
            fromVertex.neighbors.Add(toVertex); //directed edge
            toVertex.neighbors.Add(fromVertex); //undirected edge - two directed edges
        }

        public List<TLabel> GetNeighbors(TLabel label)
        {
            Vertex vertex = null; //Add missing vertex exception
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
            Vertex vertex = null; //Add missing vertex exception
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
            Vertex vertex = null; //Add missing vertex exception
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
