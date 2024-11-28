CREATE VIEW [dbo].[FullBookInfo]
	AS 
	Select  [b].[Id], [b].[Titolo], [b].[Quantita], [b].[DataPubblicazione], [b].[Genere], [b].[Prezzo], [b].[AuthorId], [a].[Nome], [a].[Cognome], [a].[Nazionalita], [a].[DoB], [a].[LuogoNascita], [a].[DoD]
	From Book b 
	Left join Author a on b.AuthorId = a.Id
