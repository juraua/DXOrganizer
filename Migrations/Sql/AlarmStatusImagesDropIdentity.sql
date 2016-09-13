ALTER TABLE [dbo].[AlarmClocks] DROP CONSTRAINT [FK_dbo.AlarmClocks_dbo.AlarmStatusImages_AlarmStatusImageId];
GO

CREATE TABLE [dbo].[tmp_ms_xx_AlarmStatusImages] (
    [AlarmStatusImageId]       INT             NOT NULL,
    [AlarmStatusImageInstance] VARBINARY (MAX) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.AlarmStatusImages] PRIMARY KEY CLUSTERED ([AlarmStatusImageId] ASC)
);

INSERT INTO [dbo].[tmp_ms_xx_AlarmStatusImages] ([AlarmStatusImageId], [AlarmStatusImageInstance])
SELECT   [AlarmStatusImageId],
            [AlarmStatusImageInstance]
FROM     [dbo].[AlarmStatusImages]
ORDER BY [AlarmStatusImageId] ASC;

DROP TABLE [dbo].[AlarmStatusImages];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AlarmStatusImages]', N'AlarmStatusImages';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.AlarmStatusImages]', N'PK_dbo.AlarmStatusImages', N'OBJECT';
GO

ALTER TABLE [dbo].[AlarmClocks] WITH NOCHECK
	ADD CONSTRAINT [FK_dbo.AlarmClocks_dbo.AlarmStatusImages_AlarmStatusImageId] FOREIGN KEY ([AlarmStatusImageId]) REFERENCES [dbo].[AlarmStatusImages] ([AlarmStatusImageId]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[AlarmClocks] WITH CHECK CHECK CONSTRAINT [FK_dbo.AlarmClocks_dbo.AlarmStatusImages_AlarmStatusImageId];
GO
