using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class ListOfContracts<T> : IEnumerator<T> where T : Contract
    {
        private ListContract head;
        private ListContract head_pointer;

        public T Current { get { return head.monster; } }// => head.monster;

        object IEnumerator.Current => head;//Current;

        //public object Current
        //{
        //    get { return head.monster; }
        //}

        class ListContract
        {
            public T monster;
            public ListContract next;
        }

        #region methods
        //public void InsertToFront(T monster) // Adding a new contract to the self-made list of contracts
        //{
        //    ListContract newContract = new ListContract();
        //    newContract.monster = monster;
        //    newContract.next = head;
        //    head = newContract;
        ////}

        public void InsertSorted(T monster)
        {
            ListContract p = head;
            if (head == null)
            {
                head = new ListContract { monster = monster };
            }
            else 
            {
                if (head.monster.CompareTo(monster) < 0)
                {
                    ListContract newContract = new ListContract { monster = monster, next = head };
                    head = newContract;
                }               
                else
                {
                    while (p.next != null && p.next.monster.CompareTo(monster) > 0)
                    {
                        p = p.next;
                    }
                    ListContract newContract = new ListContract { monster = monster, next = p.next };
                    p.next = newContract; 
                }
            }
        }

        public void RemoveFirst()
        {
            if (head != null) head = head.next;
            //head = head ?? head.next;
        }

        public Contract GetFirst()
        {
            if (head != null)
            {
                Contract contract = head.monster;
                return contract;
            }
            else
            {
                throw new ListIsEmptyException("ERROR: No quest left in the city");
            }
        }

        public void Travelsal()
        {
            ListContract p = head;
            while (p != null)
            {
                Process(p);
                p = p.next;
            }
        }

        private void Process(ListContract p) //Travelsal átmegy a listán process-t hívja, hogy csináljon valamit
        {
            // TO BE DONE
        }

        public int Count() //counts the length of the list
        {
            int count = 0;
            ListContract p = head;
            while (p != null)
            {
                count++;
                p = p.next;
            }
            return count;
        }

        public void Dispose()
        {

        }

        //public bool MoveNext()
        //{
        //    if (head == null) return false;
        //    head = head.next;
        //    return (head != null);

        //}

        public bool MoveNext()
        {
            if (head_pointer == null)
            {
                // first call
                head_pointer = head;
                return true;
            }
            else if (head_pointer.next != null)
            {
                //n'th call
                head_pointer = head_pointer.next;
                return true;
            }
            else
            {
                //last call (end of the list)
                this.Reset();
                return false;
            }
        }
        public T this[int i]
        {
            get { return SearchItem(i); }
        }

        private T SearchItem(int index)
        {
            ListContract p = head;
            int count = 0;
            while (p != null && count < index)
            {
                p = p.next;
                count++;
            }

            if (p != null && count == index)
                return p.monster;
            else
                //throw new Exception("no item was found");
                return default;
        }
        public void Reset()
        {
            head_pointer = null;
        }
        public IEnumerator<T> GetEnumerator() {
            return this;
        }
        #endregion
    }
}
