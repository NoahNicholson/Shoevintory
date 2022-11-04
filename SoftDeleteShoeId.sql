ALTER Table ShoeCollection
DROP CONSTRAINT FK__ShoeColle__ShoeI__2B3F6F97;
ALTER Table ShoeCollection
ALTER COLUMN [ShoeId] int;
ALTER Table ShoeCollection
ADD CONSTRAINT FK__ShoeColle__ShoeI__2B3F6F97
FOREIGN KEY (ShoeId) REFERENCES Shoe(Id) ON DELETE SET NULL;