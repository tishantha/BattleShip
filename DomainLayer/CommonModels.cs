using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainLayer
{
    public class GridColumn
    {
        public int ID { get; set; }
        public int Index { get; set; }
        public string Code { get; set; }
        public bool IsHit { get; set; }
        public bool IsMis { get; set; }

    }
    public class BordColumns
    {
        public BordColumns()
        {
            A = new GridColumn();
            B = new GridColumn();
            C = new GridColumn();
            D = new GridColumn();
            E = new GridColumn();
            F = new GridColumn();
            G = new GridColumn();
            H = new GridColumn();
            I = new GridColumn();
            J = new GridColumn();
        }
        public int RowID { get; set; }
        public GridColumn A { get; set; }
        public GridColumn B { get; set; }
        public GridColumn C { get; set; }
        public GridColumn D { get; set; }
        public GridColumn E { get; set; }
        public GridColumn F { get; set; }
        public GridColumn G { get; set; }
        public GridColumn H { get; set; }
        public GridColumn I { get; set; }
        public GridColumn J { get; set; }
    }

    public class Bord
    {
        public Bord()
        {
            UserGrid = new List<BordColumns>();
            ComGrid = new List<BordColumns>();
            UserShips = new List<Ship>();
            ComShips = new List<Ship>();
            UserHit = new List<GridColumn>();
            UserMis = new List<GridColumn>();
            ComHit = new List<GridColumn>();
            ComMis = new List<GridColumn>();
        }
        public List<BordColumns> UserGrid { get; set; }
        public List<BordColumns> ComGrid { get; set; }

        public List<Ship> UserShips { get; set; }
        public List<Ship> ComShips { get; set; }
        public decimal UserLife { get; set; }
        public decimal ComLife { get; set; }

        public List<GridColumn> UserHit { get; set; }
        public List<GridColumn> ComHit { get; set; }

        public List<GridColumn> UserMis { get; set; }
        public List<GridColumn> ComMis { get; set; }

        public void SetDefault()
        {
            int k = 0;
            for (int i = 0; i < 10; i++)
            {
                BordColumns b = new BordColumns();
                b.RowID = i + 1;
                b.A.Code = "A" + (i == 9 ? "10" : "0" + (i + 1));
                b.A.Index = k++;
                b.B.Code = "B" + (i == 9 ? "10" : "0" + (i + 1));
                b.B.Index = k++;
                b.C.Code = "C" + (i == 9 ? "10" : "0" + (i + 1));
                b.C.Index = k++;
                b.D.Code = "D" + (i == 9 ? "10" : "0" + (i + 1));
                b.D.Index = k++;
                b.E.Code = "E" + (i == 9 ? "10" : "0" + (i + 1));
                b.E.Index = k++;
                b.F.Code = "F" + (i == 9 ? "10" : "0" + (i + 1));
                b.F.Index = k++;
                b.G.Code = "G" + (i == 9 ? "10" : "0" + (i + 1));
                b.G.Index = k++;
                b.H.Code = "H" + (i == 9 ? "10" : "0" + (i + 1));
                b.H.Index = k++;
                b.I.Code = "I" + (i == 9 ? "10" : "0" + (i + 1));
                b.I.Index = k++;
                b.J.Code = "J" + (i == 9 ? "10" : "0" + (i + 1));
                b.J.Index = k++;

                UserGrid.Add(b);
                ComGrid.Add(b);
            }

            
            Ship s1 = new Ship();
            s1.ShipType = "B";
            s1.Columns.Add(new GridColumn() { Code = "B03" });
            s1.Columns.Add(new GridColumn() { Code = "C03" });
            s1.Columns.Add(new GridColumn() { Code = "D03" });
            s1.Columns.Add(new GridColumn() { Code = "E03" });
            s1.Columns.Add(new GridColumn() { Code = "F03" });
            ComShips.Add(s1);

            Ship s2 = new Ship();
            s2.ShipType = "D";
            s2.Columns.Add(new GridColumn() { Code = "G01" });
            s2.Columns.Add(new GridColumn() { Code = "G02" });
            s2.Columns.Add(new GridColumn() { Code = "G03" });
            s2.Columns.Add(new GridColumn() { Code = "G04" });
            ComShips.Add(s2);

            Ship s3 = new Ship();
            s3.ShipType = "D";
            s3.Columns.Add(new GridColumn() { Code = "B07" });
            s3.Columns.Add(new GridColumn() { Code = "B08" });
            s3.Columns.Add(new GridColumn() { Code = "B09" });
            s3.Columns.Add(new GridColumn() { Code = "B10" });
            ComShips.Add(s3);

        }
    }

    public class Ship
    {
        public Ship()
        {
            Columns = new List<GridColumn>();
        }
        public string ShipType { get; set; }
        public List<GridColumn> Columns { get; set; }

    }
}