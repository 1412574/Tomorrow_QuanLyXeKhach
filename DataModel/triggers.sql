CREATE TRIGGER [Trigger]
	ON [dbo].[UngVien]
	FOR INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
		UPDATE  [dbo].[UngVien]
		SET TrangThaiUV_maTT = trangThai
	END