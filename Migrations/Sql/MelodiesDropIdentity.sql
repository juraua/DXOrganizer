ALTER TABLE [dbo].[AlarmClocks] DROP CONSTRAINT [FK_dbo.AlarmClocks_dbo.Melodies_MelodyId];
GO

CREATE TABLE [dbo].[tmp_ms_xx_Melodies] (
    [MelodyId]       INT             NOT NULL,
    [MelodyInstance] VARBINARY (MAX) NULL,
    [MelodyName]     NVARCHAR (MAX)  NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Melodies] PRIMARY KEY CLUSTERED ([MelodyId] ASC)
);

INSERT INTO [dbo].[tmp_ms_xx_Melodies] ([MelodyId], [MelodyInstance], [MelodyName])
SELECT   [MelodyId],
            [MelodyInstance],
            [MelodyName]
FROM     [dbo].[Melodies]
ORDER BY [MelodyId] ASC;

DROP TABLE [dbo].[Melodies];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Melodies]', N'Melodies';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Melodies]', N'PK_dbo.Melodies', N'OBJECT';
GO

ALTER TABLE [dbo].[AlarmClocks] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AlarmClocks_dbo.Melodies_MelodyId] FOREIGN KEY ([MelodyId]) REFERENCES [dbo].[Melodies] ([MelodyId]);
GO

ALTER TABLE [dbo].[AlarmClocks] WITH CHECK CHECK CONSTRAINT [FK_dbo.AlarmClocks_dbo.Melodies_MelodyId];
GO