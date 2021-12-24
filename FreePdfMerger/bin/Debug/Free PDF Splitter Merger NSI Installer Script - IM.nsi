; -------------------------------
; Start

  !define MUI_FILE "FreePDFSplitterMerger"
  !define MUI_VERSION ""
  !define MUI_PRODUCT "FreePDFSplitterMerger"
  !define PRODUCT_SHORTCUT "Free PDF Splitter Merger"
  !define MUI_ICON "pdf_split48x48.ico"
  !define PRODUCT_VERSION "1.8"

    BrandingText "www.4dots-software.com"
;  !define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\readme.txt"

  !define MUI_CUSTOMFUNCTION_GUIINIT myGuiInit
  
  Var RevenyouProduct

  !include "MUI2.nsh"
  !include Library.nsh
  !include InstallOptions.nsh
  !include "x64.nsh"
  Name "FreePDFSplitterMerger"
  OutFile "FreePDFSplitterMergerSetup.exe" 
  
  InstallDir "$PROGRAMFILES\4dots Software\${PRODUCT_SHORTCUT}"

  InstallDirRegKey HKLM "Software\${MUI_PRODUCT}" ""

  var ALREADY_INSTALLED
  RequestExecutionLevel admin

 
;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING 
;--------------------------------
;General
 
  !insertmacro MUI_PAGE_WELCOME
;   Page custom BundlePage
  !insertmacro MUI_PAGE_LICENSE "..\..\..\license_agreement.rtf"
 ; !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY 
  !insertmacro MUI_PAGE_INSTFILES
  
  Page custom OptionsPage
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

; !define MUI_FINISHPAGE_RUN "$INSTDIR\${MUI_FILE}.exe"

  !define MUI_FINISHPAGE_RUN 
  !define MUI_FINISHPAGE_RUN_FUNCTION "OpenWebpageFunction"
  ;!define MUI_FINISHPAGE_RUN_TEXT "Open Application Webpage for Information"
   !insertmacro MUI_PAGE_FINISH

   Page custom DonatePage
  !insertmacro MUI_UNPAGE_FINISH  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English" 
;  !insertmacro MUI_LANGUAGE "Greek" 
 
;-------------------------------- 
;Installer Sections     

Function AddStartMenu
;create start-menu items
  CreateDirectory "$SMPROGRAMS\${PRODUCT_SHORTCUT}"
  CreateShortCut "$SMPROGRAMS\${PRODUCT_SHORTCUT}\${PRODUCT_SHORTCUT}.lnk" "$INSTDIR\${MUI_FILE}.exe" "" "$INSTDIR\${MUI_FILE}.exe" 0 
  CreateShortCut "$SMPROGRAMS\${PRODUCT_SHORTCUT}\Uninstall.lnk" "$INSTDIR\Uninstall.exe" "" "$INSTDIR\Uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\${PRODUCT_SHORTCUT}\Free PDF Splitter Merger - Users Manual.chm.lnk" "$INSTDIR\Free PDF Splitter Merger - Users Manual.chm" "" "$INSTDIR\Free PDF Splitter Merger - Users Manual.chm" 0

Functionend

Function AddDesktopShortcut
  CreateShortCut "$DESKTOP\${PRODUCT_SHORTCUT}.lnk" "$INSTDIR\${MUI_FILE}.exe" ""
FunctionEnd

Function IntegrateWindowsExplorer
    WriteRegStr HKCR "*\shell\FreePDFSplitterMerger" "" "Merge Pdf with FreePDFSplitterMerger"
    WriteRegStr HKCR "*\shell\FreePDFSplitterMerger\command" "" "$\"$INSTDIR\FreePDFSplitterMerger.exe$\" -merge -visual $\"%1$\""

    WriteRegStr HKCR "*\shell\FreePDFSplitterMerger2" "" "Split / Extract / Delete Pages with FreePDFSplitterMerger"
    WriteRegStr HKCR "*\shell\FreePDFSplitterMerger2\command" "" "$\"$INSTDIR\FreePDFSplitterMerger.exe$\" -split -visual $\"%1$\""

FunctionEnd

Function AddQuickLaunch
  CreateShortCut "$QUICKLAUNCH\${PRODUCT_SHORTCUT}.lnk" "$INSTDIR\${MUI_FILE}.exe" ""
FunctionEnd

Section "install" installinfo
;Add files 
  SetShellVarContext all

  SetOutPath "$INSTDIR"
;  inetc::get /SILENT "http://www.4dots-software.com/installmonetizer/FreePDFSplitterMerger.php" "$PLUGINSDIR\Installmanager.exe" /end
  ;ExecWait "$PLUGINSDIR\Installmanager.exe 018"
 
  File "..\Release\CryptoObfuscator_Output\${MUI_FILE}.exe"
 ; File "..\..\Initial Files\drm.dat"

  File "c:\code\misc\=redist\vcredist_x64.exe"
  File "c:\code\misc\=redist\vcredist_x86.exe"

 ${If} ${RunningX64}
   File /oname=FreeImage.dll "FreeImagex64.dll" 
${Else}
; 32bit things here
   File /oname=FreeImage.dll "FreeImagex86.dll" 
${EndIf}  

   File "FreeImageNET.dll"
   File "..\release\itextsharp.dll"

  File "..\..\..\manual\Free PDF Splitter Merger - Users Manual.chm";
  File "..\..\..\readme.txt"
 
  ${If} ${RunningX64}

!ifndef LIBRARY_X64
 !define LIBRARY_X64
!endif

   ExecWait '"$INSTDIR\vcredist_x64.exe" /q'
${Else}
   ExecWait '"$INSTDIR\vcredist_x86.exe" /q'
${EndIf}  


 
;create desktop shortcut

  ; CreateShortCut "$SENDTO\${PRODUCT_SHORTCUT}.lnk" "$INSTDIR\${MUI_FILE}.exe" "-OpenImage" "$INSTDIR\${MUI_FILE}.exe"
 

;write uninstall information to the registry
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_SHORTCUT}" "DisplayName" "${PRODUCT_SHORTCUT} (remove only)"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_SHORTCUT}" "DisplayIcon" "$INSTDIR\${MUI_FILE}.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_SHORTCUT}" "UninstallString" "$INSTDIR\Uninstall.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_SHORTCUT}" "Publisher" "4dots Software"   
  
 ;Store installation folder
  WriteRegStr HKLM "Software\${MUI_PRODUCT}" "" $INSTDIR
 
  WriteUninstaller "$INSTDIR\Uninstall.exe"
 
   ;inetc::get /SILENT "http://www.4dots-software.com/dolog/dolog.php?request_file=${PRODUCT_SHORTCUT}&version=${PRODUCT_VERSION}" "$PLUGINSDIR\temptmp.txt"  /end

SectionEnd
 
 
;--------------------------------    
;Uninstaller Section  
Section "Uninstall"
  SetShellVarContext all

  ExecWait "$INSTDIR\${MUI_FILE}.exe /uninstall"  
 
;Delete Files 
  RMDir /r "$INSTDIR\*.*"    
 
;Remove the installation directory
  RMDir "$INSTDIR"
 
;Delete Start Menu Shortcuts
  Delete "$DESKTOP\${PRODUCT_SHORTCUT}.lnk"
  Delete "$SENDTO\${PRODUCT_SHORTCUT}.lnk"
  Delete "$SMPROGRAMS\${PRODUCT_SHORTCUT}\*.*"
  RmDir  "$SMPROGRAMS\${PRODUCT_SHORTCUT}"
 
;Delete Uninstaller And Unistall Registry Entries
  DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\${PRODUCT_SHORTCUT}"
  DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_SHORTCUT}"  
  DeleteRegKey /ifempty HKLM "Software\${MUI_PRODUCT}"
  DeleteRegKey HKCR "*\shell\FreePDFSplitterMerger"
  DeleteRegKey HKCR "*\shell\FreePDFSplitterMerger2"

SectionEnd
 

;--------------------------------    
;MessageBox Section
 
 
;Function that calls a messagebox when installation finished correctly
Function .onInstSuccess
 ; MessageBox MB_OK "You have successfully installed ${MUI_PRODUCT}. Use the desktop icon to start the program."
 ExecShell "" "http://www.4dots-software.com/free-pdf-splitter-merger/merge-documents.php?afterinstall=true&version=${PRODUCT_VERSION}";
FunctionEnd
 
 
Function un.onUninstSuccess
  MessageBox MB_OK "You have successfully uninstalled ${MUI_PRODUCT}."
FunctionEnd
 
Function OptionsPage

  ; Prepare shortcut page with default values
  !insertmacro MUI_HEADER_TEXT "Additional Options" "Please select, whether shortcuts should be created."

  ; Display shortcut page
  !insertmacro INSTALLOPTIONS_DISPLAY_RETURN "NSISAdditionalActionsPage.ini"
  pop $R0 

  ${If} $R0 == "cancel"
    Abort
  ${EndIf} 

  ; Get the selected options

  ReadINIStr $R1 "$PLUGINSDIR\NSISAdditionalActionsPage.ini" "Field 2" "State"
  ReadINIStr $R2 "$PLUGINSDIR\NSISAdditionalActionsPage.ini" "Field 3" "State"
;  ReadINIStr $R3 "$PLUGINSDIR\NSISAdditionalActionsPage.ini" "Field 4" "State"

  ${If} $R1 == "1"  
    Call AddStartMenu
  ${EndIf}

  ${If} $R2 == "1"  
   Call AddDesktopShortcut
  ${EndIf}  

  ${If} $R3 == "1"  
 ;  Call IntegrateWindowsExplorer
  ${EndIf}  


FunctionEnd

Function myGUIInit
  SetShellVarContext all
 StrCpy $INSTDIR "$PROGRAMFILES\${PRODUCT_SHORTCUT}"
FunctionEnd 
  
Function .onInit
  !insertmacro INSTALLOPTIONS_EXTRACT "NSISAdditionalActionsPage.ini"
FunctionEnd

Function OpenWebpageFunction
  ExecShell "" "$INSTDIR\${MUI_FILE}.exe"
FunctionEnd

;Function OpenWebpageFunction
  ;ExecShell "" "http://www.4dots-software.com/free-pdf-splitter-merger/?afterinstall=true"
;FunctionEnd

;eof

Function DonatePage
   File /oname=$PLUGINSDIR\paypal-donate.bmp "C:\code\Misc\paypal-donate.bmp"
   
   Push $0
   Push $1

   !insertmacro MUI_HEADER_TEXT "Please Donate !" "Your donations are absolutely essential to us !"  
   nsDialogs::Create 1018
   Pop $0
   SetCtlColors $0 "" F0F0F0

   ${NSD_CreateBitmap} 150 50 73 44 ""
   Pop $0
   ${NSD_SetImage} $0 $PLUGINSDIR\\paypal-donate.bmp $1
   Push $1

   ; Register handler for click events
   ${NSD_OnClick} $0 DonateWebpage

   ${NSD_CreateLink} 50 120 100% 12 "Please Donate ! You donations are absolutely essential to us !"
   Pop $0
   ${NSD_OnClick} $0 DonateWebpage     
   
   nsDialogs::Show

   Pop $1
   ${NSD_FreeImage} $1

   Pop $1
   Pop $0 

FunctionEnd
 
Function DonateWebpage
	ExecShell "" "http://www.4dots-software.com/donate.php" 
FunctionEnd
