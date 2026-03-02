create view v_categories as
select C.CategoryID, C.CategoryName, C.Description, C.IsActive
from Categories C;