create view v_shippers_orders as
select s.CompanyName, T.OrderID, T.OrderDate, T.ProductID, T.Quantity
from Shippers s
         join (select o.ShipVia, o.OrderID, o.OrderDate, od.ProductID, od.Quantity
               from Orders o
                        join [Order Details] od on od.OrderID = o.OrderID) T
              on s.ShipperID = T.ShipVia 