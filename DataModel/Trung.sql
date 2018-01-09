SET IDENTITY_INSERT TrangThaiUV ON
INSERT INTO TrangThaiUV
(
    [maTT],
    [tenTT],
    [moTaTT]
)
VALUES
(1, 'Mới', ''),
(2, 'Chờ phỏng vấn', ''),
(3, 'Đậu phỏng vấn', ''),
(4, 'Rớt phỏng vấn', '');
SET IDENTITY_INSERT TrangThaiUV OFF



GO
CREATE TRIGGER [Trigger]
	ON [dbo].[UngVien]
	FOR INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
		UPDATE  [dbo].[UngVien]
		SET TrangThaiUV_maTT = trangThai
	END
GO
SET IDENTITY_INSERT UngVien ON
INSERT INTO UngVien
(
    [maUV],
    [hoTen],
    [sDT],
    [email],
    [trangThai],
    [maLPV]
)
VALUES
(1, 'Nguyễn Quang Sang', '', '', 1, null),
(2, 'Trịnh Lê Bắc', '', '', 2, 1),
(3, 'Vũ Xuân Hồng', '', '', 3, 2),
(4, 'Trần Đăng Tuấn', '', '', 4, 3);
SET IDENTITY_INSERT UngVien OFF


SET IDENTITY_INSERT LichPhongVan ON
INSERT INTO LichPhongVan
(
    [maLPV],
    [ngay],
    [diaDiem],
    [tieuChi],
    [ghiChu]
)
VALUES
(1, '22/11/2017', 'Hội trường công ty', '2 developer, 1 tester', ''),
(2, '22/12/2017', 'Hội trường công ty', '1 BA', ''),
(3, '22/01/2018', 'Hội trường công ty', '1 PM', '');
SET IDENTITY_INSERT LichPhongVan OFF
