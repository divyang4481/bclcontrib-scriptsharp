//http://fgiesen.wordpress.com/2011/07/09/a-trip-through-the-graphics-pipeline-2011-index/

//// non-portable system services
//#include "../sys/sys_public.h"

//// id lib
//#include "../idlib/Lib.h"

//// framework
//#include "../framework/BuildVersion.h"
//#include "../framework/BuildDefines.h"
//#include "../framework/Licensee.h"
//#include "../framework/CmdSystem.h"
//#include "../framework/CVarSystem.h"
//#include "../framework/Common.h"
//#include "../framework/File.h"
//#include "../framework/FileSystem.h"
//#include "../framework/UsercmdGen.h"

//// decls
//#include "../framework/DeclManager.h"
//#include "../framework/DeclTable.h"
//#include "../framework/DeclSkin.h"
//#include "../framework/DeclEntityDef.h"
//#include "../framework/DeclFX.h"
//#include "../framework/DeclParticle.h"
//#include "../framework/DeclAF.h"
//#include "../framework/DeclPDA.h"

//// We have expression parsing and evaluation code in multiple places:
//// materials, sound shaders, and guis. We should unify them.
//const int MAX_EXPRESSION_OPS = 4096;
//const int MAX_EXPRESSION_REGISTERS = 4096;

//// renderer
//#include "../renderer/qgl.h"
//#include "../renderer/Cinematic.h"
//#include "../renderer/Material.h"
//#include "../renderer/Model.h"
//#include "../renderer/ModelManager.h"
//#include "../renderer/RenderSystem.h"
//#include "../renderer/RenderWorld.h"

//// sound engine
//#include "../sound/sound.h"

//// asynchronous networking
//#include "../framework/async/NetworkSystem.h"

//// user interfaces
//#include "../ui/ListGUI.h"
//#include "../ui/UserInterface.h"

//// collision detection system
//#include "../cm/CollisionModel.h"

//// AAS files and manager
//#include "../tools/compilers/aas/AASFile.h"
//#include "../tools/compilers/aas/AASFileManager.h"

//// game
//#if defined(_D3XP)
//#include "../d3xp/Game.h"
//#else
//#include "../game/Game.h"
//#endif




////-----------------------------------------------------


//#ifdef GAME_DLL

//#if defined(_D3XP)
//#include "../d3xp/Game_local.h"
//#else
//#include "../game/Game_local.h"
//#endif

//#else

//#include "../framework/DemoChecksum.h"

//// framework
//#include "../framework/Compressor.h"
//#include "../framework/EventLoop.h"
//#include "../framework/KeyInput.h"
//#include "../framework/EditField.h"
//#include "../framework/Console.h"
//#include "../framework/DemoFile.h"
//#include "../framework/Session.h"

//// asynchronous networking
//#include "../framework/async/AsyncNetwork.h"

//// The editor entry points are always declared, but may just be
//// stubbed out on non-windows platforms.
//#include "../tools/edit_public.h"

//// Compilers for map, model, video etc. processing.
//#include "../tools/compilers/compiler_public.h"

//#endif /* !GAME_DLL */

