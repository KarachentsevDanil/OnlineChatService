CREATE OR ALTER VIEW [chat].[PrivateChatView]
AS
SELECT PrivateChat.Id, PrivateChat.CreatedByUserId, PrivateChat.InvitedUserId,
CONCAT_WS(' ', CreatedByUser.FirstName, CreatedByUser.LastName) as CreatedByUserFullName, CreatedByUser.Email as CreatedByUserEmail, CreatedByUser.IsOnline as CreatedByUserIsOnline,
CONCAT_WS(' ', InvitedUser.FirstName, InvitedUser.LastName) as InvitedUserFullName, InvitedUser.Email as InvitedUserEmail, InvitedUser.IsOnline as InvitedUserIsOnline,
LastMessage.[Text] as LastMessageText, LastMessage.CreatedByUserId as LastMessageCreatedByUserId, LastMessage.CreatedAt as LastMessageCreatedAt
FROM chat.PrivateChats as PrivateChat
	INNER JOIN [user].Users as CreatedByUser ON PrivateChat.CreatedByUserId = CreatedByUser.Id
	INNER JOIN [user].Users as InvitedUser ON PrivateChat.InvitedUserId = InvitedUser.Id
	OUTER APPLY(
	SELECT TOP 1 PrivateMessage.*
	FROM chat.PrivateChatMessages as PrivateMessage
	WHERE PrivateMessage.ChatId = PrivateChat.Id
	ORDER BY PrivateMessage.CreatedAt DESC
	) as LastMessage
WHERE PrivateChat.IsDeleted = 0