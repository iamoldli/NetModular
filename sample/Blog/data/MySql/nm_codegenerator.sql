/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : localhost:3306
 Source Schema         : nm_codegenerator

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 22/05/2019 13:15:06
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
INSERT INTO `class` VALUES ('39edefa3-4e1d-26d3-2e75-bb328f811cb4', '39edefa3-0000-8361-e658-6bbf1f77e3f1', 'Article', 'Article', 6, '文章', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39edefa3-8052-72c5-f5b4-fc5437e82252', '39edefa3-0000-8361-e658-6bbf1f77e3f1', 'Category', 'Category', 6, '文章分类', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `class` VALUES ('39edefa3-a948-15e6-5b54-78fcfb9ebb8f', '39edefa3-0000-8361-e658-6bbf1f77e3f1', 'Tag', 'Tag', 6, '文章标签', '2019-05-22 10:55:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

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
INSERT INTO `project` VALUES ('39edefa3-0000-8361-e658-6bbf1f77e3f1', '个人博客', 'Nm', 'Blog', '2019-05-22 10:54:29', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:29', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', 0, '2019-05-22 10:54:29', '00000000-0000-0000-0000-000000000000');

-- ----------------------------
-- Table structure for property
-- ----------------------------
DROP TABLE IF EXISTS `property`;
CREATE TABLE `property`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
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
INSERT INTO `property` VALUES ('39edefa3-4e2e-429b-a4a4-4634dec89a25', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-4560-12fc-ddc9c4865ce1', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-a25d-dc7c-49ca76373246', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-babc-7728-235031ebeea2', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-4e2e-cb39-340a-ee696625e7e2', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:54:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-805f-a14a-5c86-adf32ec6dc82', '39edefa3-8052-72c5-f5b4-fc5437e82252', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-805f-c84e-beb8-15c4ce852d82', '39edefa3-8052-72c5-f5b4-fc5437e82252', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-805f-fff2-8184-80884726e264', '39edefa3-8052-72c5-f5b4-fc5437e82252', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-8060-2a22-b235-02b8e1a9692e', '39edefa3-8052-72c5-f5b4-fc5437e82252', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-8060-5efa-f96a-3794c91ca327', '39edefa3-8052-72c5-f5b4-fc5437e82252', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:02', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-a95a-3fa9-c2ef-3205da03745d', '39edefa3-a948-15e6-5b54-78fcfb9ebb8f', 'CreatedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建时间', 1001, '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-a95a-5f06-2264-ab57b1177280', '39edefa3-a948-15e6-5b54-78fcfb9ebb8f', 'ModifiedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改人', 1002, '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-a95a-c75e-fee9-80082c90b373', '39edefa3-a948-15e6-5b54-78fcfb9ebb8f', 'CreatedBy', 0, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '创建人', 1000, '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-a95a-fbe1-ba98-be2eea48f2eb', '39edefa3-a948-15e6-5b54-78fcfb9ebb8f', 'Id', 1, 1, 8, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '主键', 0, '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefa3-a95a-fff0-910b-0f1d59b7a06f', '39edefa3-a948-15e6-5b54-78fcfb9ebb8f', 'ModifiedTime', 0, 1, 9, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 0, 0, '修改时间', 1003, '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 10:55:12', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefb4-afea-2a6f-d942-a8c808319c7e', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'Title', 0, 0, 0, 300, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '标题', 5, '2019-05-22 11:13:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 11:13:49', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefb4-f408-057b-d8d5-0db948a3d603', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'MediaType', 0, 0, 10, 0, 0, 0, '39edefa0-a334-d7df-006b-f918392303f3', 0, 1, 0, '多媒体类型', 6, '2019-05-22 11:14:06', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 11:14:06', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');
INSERT INTO `property` VALUES ('39edefb5-196f-a926-7b73-49e4255831a7', '39edefa3-4e1d-26d3-2e75-bb328f811cb4', 'Body', 0, 0, 0, 0, 0, 0, '00000000-0000-0000-0000-000000000000', 0, 1, 0, '内容', 7, '2019-05-22 11:14:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-05-22 11:14:16', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a');

SET FOREIGN_KEY_CHECKS = 1;
