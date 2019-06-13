/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : localhost:3306
 Source Schema         : nm_personnelfiles

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 13/06/2019 18:40:46
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for company
-- ----------------------------
DROP TABLE IF EXISTS `company`;
CREATE TABLE `company`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `Address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '地址',
  `Contact` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '联系人',
  `Phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '联系电话',
  `Fax` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '传真',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of company
-- ----------------------------
INSERT INTO `company` VALUES ('39ee6189-b90f-fd20-ee94-a44b81c6d6f4', '南京欧德利科技有限公司', '南京', '', '48484848', '', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 13:43:35', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 13:45:39');
INSERT INTO `company` VALUES ('39ee61a7-9727-5fc5-e5eb-aa70c2512b18', '微软科技', '西雅图', '', '', '', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 14:16:13', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 14:16:13');
INSERT INTO `company` VALUES ('39ee61a7-b87b-5769-8324-6341835f0939', '苹果科技', '纽约', '', '', '', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 14:16:21', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 14:16:21');

-- ----------------------------
-- Table structure for department
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `CompanyId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '公司编号',
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `ParentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '父节点',
  `Leader` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '负责人',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of department
-- ----------------------------
INSERT INTO `department` VALUES ('39ee628d-ca5d-7aa8-d79d-5813583943f4', '39ee6189-b90f-fd20-ee94-a44b81c6d6f4', '研发部', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', 0, '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 18:27:39', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 18:33:50');
INSERT INTO `department` VALUES ('39ee628e-109c-80f9-4615-d01360c67014', '39ee6189-b90f-fd20-ee94-a44b81c6d6f4', 'Java项目组', '39ee628d-ca5d-7aa8-d79d-5813583943f4', '00000000-0000-0000-0000-000000000000', 0, '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 18:27:57', '2e23d1d9-4a72-acc2-f6ff-39ed21cb6a4a', '2019-06-13 18:27:57');

-- ----------------------------
-- Table structure for position
-- ----------------------------
DROP TABLE IF EXISTS `position`;
CREATE TABLE `position`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `DepartmentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '部门编码',
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `Number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '工号',
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `DepartmentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '所属部门',
  `PositionId` int(11) NOT NULL COMMENT '职位编号',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '姓名',
  `Sex` tinyint(3) NOT NULL COMMENT '性别',
  `Native` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '籍贯',
  `Birthday` datetime(0) NULL DEFAULT NULL COMMENT '出生日期',
  `Nation` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '民族',
  `Education` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '学历',
  `Picture` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '照片',
  `CreatedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `CreatedTime` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifiedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改人',
  `ModifiedTime` datetime(0) NOT NULL COMMENT '修改时间',
  `Deleted` tinyint(1) NOT NULL COMMENT '已删除',
  `DeletedTime` datetime(0) NOT NULL COMMENT '删除时间',
  `DeletedBy` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '删除人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for usercontact
-- ----------------------------
DROP TABLE IF EXISTS `usercontact`;
CREATE TABLE `usercontact`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户编号',
  `Phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '手机号',
  `Email` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `QQ` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'QQ',
  `Wechat` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '微信',
  `DingDing` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '钉钉',
  `ProvinceCode` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '省份编码',
  `ProvinceName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '省份名称',
  `CityCode` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '城市编码',
  `CityName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '城市名称',
  `AreaCode` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '区县编码',
  `AreaName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '区县名称',
  `TownCode` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '城镇编码',
  `TownName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '城镇名称',
  `Address` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '详细地址',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for usereducationhistory
-- ----------------------------
DROP TABLE IF EXISTS `usereducationhistory`;
CREATE TABLE `usereducationhistory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户编号',
  `SchoolName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '学校名称',
  `Level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '学历',
  `StartDate` datetime(0) NOT NULL COMMENT '开始日期',
  `EndDate` datetime(0) NOT NULL COMMENT '结束日期',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for userworkhistory
-- ----------------------------
DROP TABLE IF EXISTS `userworkhistory`;
CREATE TABLE `userworkhistory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户编号',
  `EnterpriseName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '企业名称',
  `StartDate` datetime(0) NOT NULL COMMENT '起始日期',
  `EndDate` datetime(0) NOT NULL COMMENT '结束日期',
  `Position` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '职位',
  `Contact` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '联系人',
  `ContactPhone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '联系人手机号',
  `DimissionReason` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '离职原因',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
