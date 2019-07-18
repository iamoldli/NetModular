/*
 Navicat Premium Data Transfer

 Source Server         : 129.211.40.240
 Source Server Type    : MySQL
 Source Server Version : 80016
 Source Host           : 129.211.40.240:13306
 Source Schema         : nm_codegenerator

 Target Server Type    : MySQL
 Target Server Version : 80016
 File Encoding         : 65001

 Date: 18/07/2019 15:08:45
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for class
-- ----------------------------
DROP TABLE IF EXISTS `class`;
CREATE TABLE `class`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProjectId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '项目编号',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `TableName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '表明',
  `BaseEntityType` smallint(6) NOT NULL COMMENT '基类类型',
  `Remarks` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '说明',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of class
-- ----------------------------
INSERT INTO `class` VALUES ('39edefa3-4e1d-26d3-2e75-bb328f811cb4', '39edefa3-0000-8361-e658-6bbf1f77e3f1', 'Article', 'Article', 6, '文章', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:42:33', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39ee2dfd-f422-523b-a225-0ea038951ffc', '39edefa3-0000-8361-e658-6bbf1f77e3f1', 'Category', 'Category', 6, '分类', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39ee2e01-e091-7ada-59d1-efb5a4feb961', '39edefa3-0000-8361-e658-6bbf1f77e3f1', 'Tag', 'Tag', 6, '标签', '2019-06-03 13:34:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-05 01:41:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39eee7b5-b64c-ffdd-1f58-ebb9d335706c', '39ee7708-0300-4577-844f-bd0fa7d6b0b0', 'test', 'test', 6, 'test', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39ef0281-7aa7-02de-514a-a7d764b4296f', '39ef027f-bbda-648a-ee83-2f8f2d126516', 'AppLink', 'app_link', 4, 'AppLink', '2019-07-14 19:53:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 20:02:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39ef0592-16d3-26c4-6249-38c49c76d927', '39ef058f-fd8e-b7bf-dbe0-541d949a0364', 'Member', 'member', 4, '会员表', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for class_method
-- ----------------------------
DROP TABLE IF EXISTS `class_method`;
CREATE TABLE `class_method`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ClassId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '实体编号',
  `Query` tinyint(1) NOT NULL DEFAULT 1 COMMENT '查询列表',
  `Add` tinyint(1) NOT NULL DEFAULT 1 COMMENT '添加',
  `Delete` tinyint(1) NOT NULL DEFAULT 1 COMMENT '删除',
  `Edit` tinyint(1) NOT NULL DEFAULT 1 COMMENT '编辑',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of class_method
-- ----------------------------
INSERT INTO `class_method` VALUES ('39ee2dfd-f427-6c9f-48db-8c19a9d1fdb0', '39ee2dfd-f422-523b-a225-0ea038951ffc', 1, 1, 1, 1, '2019-06-03 13:30:18', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:18', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class_method` VALUES ('39ee2e01-e0bf-c4a2-0f09-f8993f5b3bb8', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 1, 1, 1, 1, '2019-06-03 13:34:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-05 01:41:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class_method` VALUES ('39ee2e09-2c77-4acb-287b-c292afc10a5f', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 0, 1, 1, 0, '2019-06-03 13:42:33', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:42:33', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class_method` VALUES ('39eee7b5-b654-9996-bec2-fda33fd39a85', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 1, 1, 1, 1, '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class_method` VALUES ('39ef0281-7aa9-258c-9507-5c3ca698e9bb', '39ef0281-7aa7-02de-514a-a7d764b4296f', 1, 1, 1, 1, '2019-07-14 19:53:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 20:02:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class_method` VALUES ('39ef0592-16d5-1080-5928-720aa44a60ae', '39ef0592-16d3-26c4-6249-38c49c76d927', 1, 1, 1, 1, '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for enum
-- ----------------------------
DROP TABLE IF EXISTS `enum`;
CREATE TABLE `enum`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Remarks` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of enum
-- ----------------------------
INSERT INTO `enum` VALUES ('39edefa0-a334-d7df-006b-f918392303f3', 'MediaType', '多媒体类型', '2019-05-22 10:51:55', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:51:55', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `enum` VALUES ('39eee8a3-4175-36a9-2282-02caef9904f8', 'aa', 'aa', '2019-07-09 19:20:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 19:20:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for enum_item
-- ----------------------------
DROP TABLE IF EXISTS `enum_item`;
CREATE TABLE `enum_item`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `EnumId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '所属枚举',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Value` int(11) NOT NULL COMMENT '值',
  `Remarks` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of enum_item
-- ----------------------------
INSERT INTO `enum_item` VALUES ('39edef93-19b3-938c-81f1-6310a08a2fdf', '39edef92-cf05-1f53-e620-e7af8c30edb3', 'Picture', 1, '图片');
INSERT INTO `enum_item` VALUES ('39edef93-4435-cce0-b952-c5fc0fcc26d7', '39edef92-cf05-1f53-e620-e7af8c30edb3', 'Video', 2, '视频');
INSERT INTO `enum_item` VALUES ('39edef93-6d69-b19d-9eca-2ee877778335', '39edef92-cf05-1f53-e620-e7af8c30edb3', 'Music', 3, '音乐');
INSERT INTO `enum_item` VALUES ('39edefa0-cb9e-31aa-3cef-4cbaaac1f078', '39edefa0-a334-d7df-006b-f918392303f3', 'Img', 1, '图片');
INSERT INTO `enum_item` VALUES ('39edefa0-ed3a-ca44-43f8-3dd648d35ffb', '39edefa0-a334-d7df-006b-f918392303f3', 'Video', 2, '视频');
INSERT INTO `enum_item` VALUES ('5BE87919-DFEF-E657-4FAC-39EDC1C50A92', 'D1990B44-2130-1C3B-9DD7-39EDA3D09BAA', 'Audio', 2, '音乐');
INSERT INTO `enum_item` VALUES ('80D98D49-A685-ADFC-9BF2-39EDC650E37D', 'D1990B44-2130-1C3B-9DD7-39EDA3D09BAA', 'Video', 3, '视频');
INSERT INTO `enum_item` VALUES ('CEA1D415-751C-7493-D6B2-39EDC1C4CDF9', 'D1990B44-2130-1C3B-9DD7-39EDA3D09BAA', 'Img', 1, '图片');

-- ----------------------------
-- Table structure for model_property
-- ----------------------------
DROP TABLE IF EXISTS `model_property`;
CREATE TABLE `model_property`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProjectId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '项目编号',
  `ClassId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` smallint(6) NOT NULL,
  `ModelType` smallint(6) NOT NULL COMMENT '模型类型',
  `Nullable` tinyint(4) NOT NULL COMMENT '可空',
  `EnumId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remarks` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of model_property
-- ----------------------------
INSERT INTO `model_property` VALUES ('39eee7b6-13ab-ac99-4290-8050a9c3399a', '', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 'test', 0, 1, 0, '00000000-0000-0000-0000-000000000000', 'test', 0, '2019-07-09 15:01:09', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:01:09', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0284-1dbf-dc5f-ce97-bcc804c82b05', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'Url', 0, 1, 0, '00000000-0000-0000-0000-000000000000', 'Url', 0, '2019-07-14 19:56:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:56:20', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0284-4d66-e531-b789-c87a8bccee6b', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'Url', 0, 2, 0, '00000000-0000-0000-0000-000000000000', 'Url', 0, '2019-07-14 19:56:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:56:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0284-6868-b6aa-9005-7b0a97206f99', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'Url', 0, 3, 0, '00000000-0000-0000-0000-000000000000', 'Url', 0, '2019-07-14 19:56:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:56:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0657-f740-9038-f523-b58ec369a266', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberPhone', 0, 1, 0, '00000000-0000-0000-0000-000000000000', '会员手机号', 2, '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0657-f740-aa74-4bd0-ea06334e069b', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberRank', 3, 1, 0, '00000000-0000-0000-0000-000000000000', '会员级别', 5, '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0657-f740-bf42-8e80-fbf0a59f8c0c', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberName', 0, 1, 0, '00000000-0000-0000-0000-000000000000', '会员姓名', 1, '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0657-f740-c0cb-abf4-b5f357d0eeac', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Sex', 2, 1, 1, '00000000-0000-0000-0000-000000000000', '性别', 4, '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0657-f740-de51-517f-81e9ec351a8b', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Email', 0, 1, 1, '00000000-0000-0000-0000-000000000000', '邮箱', 3, '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0657-f740-fc30-9ab2-c7a1b8f8d203', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberCode', 0, 1, 0, '00000000-0000-0000-0000-000000000000', '会员编号', 0, '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:46:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-2f56-486d-056dd4684fea', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'AreaId', 3, 2, 1, '00000000-0000-0000-0000-000000000000', '区域ID', 8, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-3cca-8091-fafa590a75e7', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'CityId', 3, 2, 1, '00000000-0000-0000-0000-000000000000', '城市ID', 7, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-3d23-e05c-8426d82880f9', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberRank', 3, 2, 0, '00000000-0000-0000-0000-000000000000', '会员级别', 5, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-5684-b55b-20aef1f994c9', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Address', 0, 2, 1, '00000000-0000-0000-0000-000000000000', '地址', 9, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-595f-af62-90d45dc4fb1b', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberCode', 0, 2, 0, '00000000-0000-0000-0000-000000000000', '会员编号', 0, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-6356-71ba-9445dd72e18c', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Email', 0, 2, 1, '00000000-0000-0000-0000-000000000000', '邮箱', 3, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-9388-0301-3138ace20ccd', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberPhone', 0, 2, 0, '00000000-0000-0000-0000-000000000000', '会员手机号', 2, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-b390-3ecb-b96c67b25626', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'ProvinceId', 3, 2, 1, '00000000-0000-0000-0000-000000000000', '省份ID', 6, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-b8ad-6291-5430b59de0bf', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberName', 0, 2, 0, '00000000-0000-0000-0000-000000000000', '会员姓名', 1, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-65e6-d3bc-2a01-00833625f804', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Sex', 2, 2, 1, '00000000-0000-0000-0000-000000000000', '性别', 4, '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-299b-499b-f541ccd2cf18', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'CityId', 3, 3, 1, '00000000-0000-0000-0000-000000000000', '城市ID', 5, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-60f9-5bc9-8ab66c515564', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Email', 0, 3, 1, '00000000-0000-0000-0000-000000000000', '邮箱', 2, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-6cfd-a96d-fbf75980afbf', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Address', 0, 3, 1, '00000000-0000-0000-0000-000000000000', '地址', 7, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-729b-c7cc-c17302a5b3ce', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'ProvinceId', 3, 3, 1, '00000000-0000-0000-0000-000000000000', '省份ID', 4, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-74ef-6e18-cbba1986d74b', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Sex', 2, 3, 1, '00000000-0000-0000-0000-000000000000', '性别', 3, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-811c-223c-1d6e16ac1be4', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'AreaId', 3, 3, 1, '00000000-0000-0000-0000-000000000000', '区域ID', 6, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-9fe8-2a07-2839ff49a6f3', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberPhone', 0, 3, 0, '00000000-0000-0000-0000-000000000000', '会员手机号', 1, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0658-d722-b30f-e96d-7bcbe8870f1d', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberName', 0, 3, 0, '00000000-0000-0000-0000-000000000000', '会员姓名', 0, '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 13:47:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0f7f-d4be-8922-6f70-e39ff82c4d7b', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'CreatedBy', 8, 2, 0, '00000000-0000-0000-0000-000000000000', '创建人', 1, '2019-07-17 08:26:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-17 08:26:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `model_property` VALUES ('39ef0f7f-d4be-b77f-5466-6273cc14bab5', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'MediaType', 10, 2, 0, '39edefa0-a334-d7df-006b-f918392303f3', '多媒体类型', 0, '2019-07-17 08:26:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-17 08:26:42', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

-- ----------------------------
-- Table structure for project
-- ----------------------------
DROP TABLE IF EXISTS `project`;
CREATE TABLE `project`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '项目名称',
  `Prefix` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '前缀',
  `Code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '编码',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `Deleted` tinyint(4) NOT NULL,
  `DeletedTime` datetime(0) NOT NULL,
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of project
-- ----------------------------
INSERT INTO `project` VALUES ('39edefa3-0000-8361-e658-6bbf1f77e3f1', '个人博客', 'Nm', 'Blog', '2019-05-22 10:54:29', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-29 11:21:59', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 0, '2019-05-22 10:54:29', '00000000-0000-0000-0000-000000000000');
INSERT INTO `project` VALUES ('39ee7708-0300-4577-844f-bd0fa7d6b0b0', 'aaaa', '', '001', '2019-06-17 17:53:33', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-13 10:19:59', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 0, '2019-06-17 17:53:33', '00000000-0000-0000-0000-000000000000');
INSERT INTO `project` VALUES ('39ee8081-667e-4543-e773-ecbc589534ba', 'Test', 'Test', '1001', '2019-06-19 14:02:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-21 15:04:01', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 1, '2019-06-24 11:22:44', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `project` VALUES ('39eefc50-d461-9a10-3af0-2b7fd0bb60d8', 'kkkkkk', '', 'foollfool', '2019-07-13 15:02:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-13 15:02:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 0, '2019-07-13 15:02:35', '00000000-0000-0000-0000-000000000000');
INSERT INTO `project` VALUES ('39ef027f-bbda-648a-ee83-2f8f2d126516', 'App', 'Nm', 'App', '2019-07-14 19:51:33', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 20:01:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 1, '2019-07-15 13:45:21', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `project` VALUES ('39ef058f-fd8e-b7bf-dbe0-541d949a0364', 'Freight', 'Nm', 'Freight', '2019-07-15 10:08:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:08:10', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 0, '2019-07-15 10:08:10', '00000000-0000-0000-0000-000000000000');
INSERT INTO `project` VALUES ('39ef10b9-e595-1863-563b-3ba6f6069e37', 'Lwh_Test', '', 'Lwh_Test', '2019-07-17 14:09:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-17 14:09:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 0, '2019-07-17 14:09:45', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for property
-- ----------------------------
DROP TABLE IF EXISTS `property`;
CREATE TABLE `property`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProjectId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '项目编号',
  `ClassId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `IsPrimaryKey` tinyint(4) NOT NULL COMMENT '是否主键',
  `IsInherit` tinyint(4) NOT NULL COMMENT '是否继承自基类实体',
  `Type` smallint(6) NOT NULL,
  `Length` int(11) NOT NULL COMMENT '长度',
  `Precision` int(11) NOT NULL COMMENT '精度',
  `Scale` int(11) NOT NULL COMMENT '刻度',
  `EnumId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '关联枚举',
  `Nullable` tinyint(4) NOT NULL COMMENT '可空',
  `ShowInList` tinyint(4) NOT NULL COMMENT '列表中显示',
  `HasDefaultValue` tinyint(4) NOT NULL COMMENT '有默认值',
  `Remarks` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of property
-- ----------------------------
INSERT INTO `property` VALUES ('39edefa3-4e2e-429b-a4a4-4634dec89a25', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-4560-12fc-ddc9c4865ce1', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-a25d-dc7c-49ca76373246', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-babc-7728-235031ebeea2', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-cb39-340a-ee696625e7e2', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefb4-afea-2a6f-d942-a8c808319c7e', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'Title', 0, 0, 0, 300, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '标题', 0, '2019-05-22 11:13:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:31:40', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefb4-f408-057b-d8d5-0db948a3d603', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'MediaType', 0, 0, 10, 0, 0, 0, '39edefa0-a334-d7df-006b-f918392303f3', 0, 1, 0, '多媒体类型', 2, '2019-05-22 11:14:06', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:31:40', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefb5-196f-a926-7b73-49e4255831a7', '', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'Body', 0, 0, 0, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '内容', 1, '2019-05-22 11:14:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:31:40', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2dfd-f425-2fe2-6add-59b5a8ea6acb', '', '39ee2dfd-f422-523b-a225-0ea038951ffc', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2dfd-f425-3da1-dd48-5fec762e47ff', '', '39ee2dfd-f422-523b-a225-0ea038951ffc', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2dfd-f425-77c3-5c3a-dd712edd5971', '', '39ee2dfd-f422-523b-a225-0ea038951ffc', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2dfd-f425-87eb-8e33-6714af4933a6', '', '39ee2dfd-f422-523b-a225-0ea038951ffc', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2dfd-f425-f29a-30cc-a6d2f1597d0e', '', '39ee2dfd-f422-523b-a225-0ea038951ffc', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:30:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2dfe-de16-4545-eec5-5818a078a66d', '', '39ee2dfd-f422-523b-a225-0ea038951ffc', 'Name', 0, 0, 0, 100, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '名称', 0, '2019-06-03 13:31:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:31:52', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2e01-e0b7-4231-fc56-74efdb6c7904', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2e01-e0b8-09a2-8dbb-13e9d7e7fa6f', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2e01-e0b8-1929-355c-f6a513d3e734', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2e01-e0b8-8894-8ead-7a19d2168505', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2e01-e0b8-d4a2-9e5c-ced2ac415be1', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-03 13:34:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ee2e02-3c3f-f56f-d1a8-d29f584c90e0', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', 'Name', 0, 0, 0, 100, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '名称', 0, '2019-06-03 13:34:58', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-11 11:05:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39eee7b5-b652-fdc9-e119-1d846af65164', '', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39eee7b5-b653-09bf-4e9c-6eb508558cd8', '', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39eee7b5-b653-0a0e-5077-4f7efbf8bcff', '', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39eee7b5-b653-4b28-96aa-26491c291db4', '', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39eee7b5-b653-5c82-f01b-3a62223d6a06', '', '39eee7b5-b64c-ffdd-1f58-ebb9d335706c', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-09 15:00:45', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39eef12a-6193-d445-dc78-ff2777a7dc0c', '', '39ee2e01-e091-7ada-59d1-efb5a4feb961', '哈哈', 0, 0, 0, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '发', 1, '2019-07-11 11:04:46', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-11 11:05:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0281-7aa8-244b-a002-97b4cb50ddd3', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0281-7aa8-b668-d431-b275228274d1', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0281-7aa8-d13f-8e0f-b20dea7139ad', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0281-7aa8-f331-0e27-1c7f7706a3a6', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0281-7aa8-f448-bc9a-30ab5d8e280a', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'Id', 1, 1, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:53:26', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0283-05fd-837d-788e-bd194e78b4ec', '', '39ef0281-7aa7-02de-514a-a7d764b4296f', 'Url', 0, 0, 0, 512, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, 'AppUrl', 5, '2019-07-14 19:55:08', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-14 19:55:08', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-16d4-18da-8f8b-ae15e2b03748', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-16d4-191e-786f-57408faec998', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Id', 1, 1, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-16d4-2a6b-bbba-16535228446d', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-16d4-c367-6ef3-b476824ddc06', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-16d4-cdb6-e9fc-adf94bdb102b', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:10:27', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-86f5-6af9-ba59-eda70034c959', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberCode', 0, 0, 0, 50, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '会员编号', 5, '2019-07-15 10:10:56', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 11:39:00', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0592-efe1-9dd9-c982-795928651c14', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberName', 0, 0, 0, 50, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '会员姓名', 6, '2019-07-15 10:11:23', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 11:39:04', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0593-2801-404c-dacc-f9eb2bd1bac7', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberPhone', 0, 0, 0, 32, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '会员手机号', 7, '2019-07-15 10:11:37', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 11:39:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0593-7831-d1d5-5d63-3741568e9d46', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Email', 0, 0, 0, 32, 0, 0, '00000000-0000-0000-0000-000000000000', 1, 1, 0, '邮箱', 8, '2019-07-15 10:11:58', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 11:39:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0593-ffa8-5984-762b-adf9b22f6a74', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Sex', 0, 0, 2, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 1, 1, 0, '性别', 9, '2019-07-15 10:12:32', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:15:36', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0594-3a0f-3657-7036-c9305814e642', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Integral', 0, 0, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 1, '积分', 10, '2019-07-15 10:12:47', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:15:38', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0594-c031-f99f-64a1-74237279bed0', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Consumption', 0, 0, 6, 0, 18, 4, '00000000-0000-0000-0000-000000000000', 0, 1, 1, '累计消费', 11, '2019-07-15 10:13:22', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:13:22', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0595-0898-77b3-0688-0f3a10e648c9', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'MemberRank', 0, 0, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 1, '会员级别', 12, '2019-07-15 10:13:40', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:13:40', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0595-63c0-7040-d798-b9fba604b6b2', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'ProvinceId', 0, 0, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 1, 1, 0, '省份ID', 13, '2019-07-15 10:14:03', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:15:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0595-97f1-432d-340b-81f8f0f48616', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'CityId', 0, 0, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 1, 1, 0, '城市ID', 14, '2019-07-15 10:14:17', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:15:43', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0595-db1d-0827-d7db-6494281ceaef', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'AreaId', 0, 0, 3, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 1, 1, 0, '区域ID', 15, '2019-07-15 10:14:34', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:15:46', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39ef0596-9348-c3c2-9678-b538df4f7adb', '', '39ef0592-16d3-26c4-6249-38c49c76d927', 'Address', 0, 0, 0, 512, 0, 0, '00000000-0000-0000-0000-000000000000', 1, 1, 0, '地址', 16, '2019-07-15 10:15:21', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-07-15 10:15:47', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

SET FOREIGN_KEY_CHECKS = 1;
