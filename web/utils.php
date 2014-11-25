<?php

class utils{
	const REPLY_CODE_OK = 1;
	const REPLY_CODE_UNKNOWN_USER = 2;
	const REPLY_CODE_REQUEST_ERROR = 3;
	public static function jsonResponse($data)
	{
		header('Content-Type: application/json; charset=utf-8');
		//header('Content-Type: text/html; charset=utf-8');
		echo json_encode($data, JSON_FORCE_OBJECT);
		//var_dump(json_decode(json_encode($data)));
	}
	
	public static function sendReply($mData, $sMessage, $iCode = self::REPLY_CODE_OK)
	{
		$aReply = array('data' => $mData, 'message' => $sMessage, 'code' => $iCode);
		utils::jsonResponse($aReply);
		return;
	}
}

