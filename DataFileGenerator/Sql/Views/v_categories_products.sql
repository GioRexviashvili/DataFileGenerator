create view v_categories_products as
select c.CategoryName                                              as CategoryName,
       cast(1 as bit)                                              as CategoryIsActive,
       p.ProductId                                                 as ProductCode,
       p.ProductName                                               as ProductName,
       p.UnitPrice                                                 as Price,
       p.UnitsInStock                                              as Quantity,
       cast(case when p.Discontinued = 1 then 0 else 1 end as bit) as ProductIsActive
from Products as p
         join Categories as c
              on c.CategoryID = p.CategoryID;