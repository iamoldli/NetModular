/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50726
Source Host           : localhost:3306
Source Database       : nm_admin

Target Server Type    : MYSQL
Target Server Version : 50726
File Encoding         : 65001

Date: 2019-08-09 19:55:08
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for auditinfo
-- ----------------------------
DROP TABLE IF EXISTS `auditinfo`;
CREATE TABLE `auditinfo` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `AccountId` char(36) DEFAULT NULL COMMENT '账户编号',
  `Area` varchar(50) NOT NULL COMMENT '区域',
  `Controller` varchar(50) NOT NULL COMMENT '控制器',
  `ControllerDesc` varchar(50) DEFAULT NULL COMMENT '控制器说明',
  `Action` varchar(50) NOT NULL COMMENT '操作',
  `ActionDesc` varchar(50) DEFAULT NULL COMMENT '操作说明',
  `Parameters` text COMMENT '参数',
  `Result` text COMMENT '返回数据',
  `ExecutionTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '方法开始执行时间',
  `ExecutionDuration` bigint(20) NOT NULL COMMENT '方法执行总用时(ms)',
  `BrowserInfo` text COMMENT '浏览器信息',
  `Platform` smallint(6) NOT NULL COMMENT '平台',
  `IP` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=894 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC;
